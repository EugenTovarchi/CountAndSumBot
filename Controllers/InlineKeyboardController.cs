using CountAndSumBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CountAndSumBot.Controllers;

internal class InlineKeyboardController
{
    private readonly IStorage _memoryStorage;
    private readonly ITelegramBotClient _telegramClient;

    public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
    {
        _telegramClient = telegramBotClient;
        _memoryStorage = memoryStorage;
    }
    
    public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
    {
        if (callbackQuery?.Data == null)
            return;

        // Обновление пользовательской сессии новыми данными
        _memoryStorage.GetSession(callbackQuery.From.Id).Option = callbackQuery.Data;

        // Генерим информационное сообщение
        string choose = callbackQuery.Data switch
        {
            "Length" => "Счёт символов",
            "Sum" => "Сумируем цифры",
            _ => String.Empty
        };
        
        // Отправляем в ответ уведомление о выборе
        await _telegramClient.SendMessage(callbackQuery.From.Id,
            $"<b>Вы выбрали - {choose}.{Environment.NewLine}</b>" + $"{choose} - можно сменить в главном меню.",
            cancellationToken: ct, parseMode: ParseMode.Html);

        if(choose == "Счёт символов")
        {
            TextMessageController.userCommand = "Length";
        }
        else if (choose == "Сумируем цифры")
        {
            TextMessageController.userCommand = "Sum";
        }
    }
}
