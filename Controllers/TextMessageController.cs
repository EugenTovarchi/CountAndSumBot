using System.Security.Cryptography.X509Certificates;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CountAndSumBot.Controllers;

internal class TextMessageController
{
    private readonly ITelegramBotClient _telegramClient;
    private readonly SumController _sumController;
    private readonly LengthController _lengthController;
    public static string userCommand;

    public TextMessageController(ITelegramBotClient telegramBotClient, SumController sumController, LengthController lengthController)
    {
        _telegramClient = telegramBotClient;
        _sumController = sumController;
        _lengthController = lengthController;
    }

    public async Task Handle(Message message, CancellationToken ct)
    {

        switch (message.Text)
        {
            case "/start":

                // Объект, представляющий кнопки
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
                {
                        InlineKeyboardButton.WithCallbackData($"Счёт символов" , $"count"),
                        InlineKeyboardButton.WithCallbackData($"Сумируем цифры" , $"sum"),
                    });

                // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                await _telegramClient.SendMessage(message.Chat.Id, $"<b>  Наш бот считает кол-во символов.</b>" +
                    $"{Environment.NewLine}" +
                    $"{Environment.NewLine}А также может суммировать ваши цифры.{Environment.NewLine}",
                    cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                break;

            default:
                await _telegramClient.SendMessage(message.Chat.Id, "Выберите опцию и напишите текст или цифры.", cancellationToken: ct);
                break;
        }
        switch (userCommand)
        {
            case "count":
                {
                    await _telegramClient.SendMessage(message.Chat.Id, $"Длина сообщения: {message.Text.Length} знаков",
            cancellationToken: ct);
                    //await _telegramClient.SendMessage(message.From.Id, $"Кол-во ваших символов: {_lengthController.Handle(message, ct)}");
                    break;
                }
            case "sum":
                {
                    await _telegramClient.SendMessage(message.From.Id, $"Сумма ваших цифр: {_sumController.Handle(message, ct)}");
                    break;
                }
        }

    }
}
