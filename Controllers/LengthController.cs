using CountAndSumBot.Configuration;
using CountAndSumBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CountAndSumBot.Controllers;

public class LengthController
{
    private readonly AppSettings _appSettings;
    private readonly ITelegramBotClient _telegramClient;
    private readonly IFileHandler _textFileHandler;
    private readonly IStorage _memoryStorage;

    public LengthController(AppSettings appSettings, ITelegramBotClient telegramBotClient,
        IFileHandler textFileHandler, IStorage memoryStorage)
    {
        _appSettings = appSettings;
        _telegramClient = telegramBotClient;
        _textFileHandler = textFileHandler;
        _memoryStorage = memoryStorage;
    }

    public async Task Handle(Message message, CancellationToken ct)
    {
        Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
        await _telegramClient.SendMessage(message.Chat.Id, $"Длина сообщения: {message.Text.Length} знаков",
            cancellationToken: ct);
    }
}
