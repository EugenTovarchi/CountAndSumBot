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
        if (string.IsNullOrEmpty(message.Text))
        {
            await _telegramClient.SendMessage(message.Chat.Id, "Сообщение не содержит текста. Пожалуйста, введите текст.", cancellationToken: ct);
            return;
        }
        //var fileId = message.Text;
        //if (fileId == null)
        //    return;
        // Извлекаем текст от пользователя
        string userText = message.Text;

        string userOption = _memoryStorage.GetSession(message.Chat.Id).Option; // Здесь получим опцию из сессии пользователя

        // Вызываем метод Download для записи текста в файл
        await _textFileHandler.Download(fileId: null, userText, ct);

        var result = _textFileHandler.Process(userOption); // Запустим обработку
        await _telegramClient.SendMessage(message.Chat.Id, result , cancellationToken: ct);
    }
}
