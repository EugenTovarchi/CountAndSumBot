using CountAndSumBot.Services;
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
                        InlineKeyboardButton.WithCallbackData($"Счёт символов" , $"Length"),
                        InlineKeyboardButton.WithCallbackData($"Сумируем цифры" , $"Sum"),
                    });

                // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                await _telegramClient.SendMessage(message.Chat.Id, $"<b>  Наш бот считает кол-во символов.</b>" +
                    $"{Environment.NewLine}" +
                    $"{Environment.NewLine}А также может суммировать ваши цифры.{Environment.NewLine}",
                    cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                break;
        }

        if (userCommand == "Length")
        {
            int lengthResult = await _lengthController.Handle(message, ct);
            await _telegramClient.SendMessage(message.Chat.Id, $"Кол-во ваших символов: {lengthResult}", cancellationToken: ct);
        }
        else if (userCommand == "Sum")
        {
            string sumResult = await _sumController.Handle(message, ct);
            await _telegramClient.SendMessage(chatId: message.Chat.Id,text: sumResult, cancellationToken: ct);
        }
        else
        {
            await _telegramClient.SendMessage(message.Chat.Id, "Выберите действие, используя кнопки ниже.", cancellationToken: ct);
        }
    }
}
