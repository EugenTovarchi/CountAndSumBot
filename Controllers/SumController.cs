using CountAndSumBot.Configuration;
using CountAndSumBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace CountAndSumBot.Controllers;

public class SumController
{
    private readonly AppSettings _appSettings;
    private readonly ITelegramBotClient _telegramClient;
    private readonly IFileHandler _textFileHandler;
    private readonly IStorage _memoryStorage; 

    public SumController(AppSettings appSettings, ITelegramBotClient telegramBotClient,
        IFileHandler textFileHandler, IStorage memoryStorage)
    {
        _appSettings = appSettings;
        _telegramClient = telegramBotClient;
        _textFileHandler = textFileHandler;
        _memoryStorage = memoryStorage; 
    }

    public async Task Handle(Message message, CancellationToken ct)
    {
        var fileId = message.Text;
        if (fileId == null)
            return;

        await _textFileHandler.Download(fileId, ct);

        string userOption = _memoryStorage.GetSession(message.Chat.Id).Option; // Здесь получим опцию из сессии пользователя
        var result = _textFileHandler.Process(userOption); // Запустим обработку
        await _telegramClient.SendMessage(message.Chat.Id, result , cancellationToken: ct);
    }
}
