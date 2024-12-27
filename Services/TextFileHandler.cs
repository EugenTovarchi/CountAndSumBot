using CountAndSumBot.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using CountAndSumBot.Utilities;

namespace CountAndSumBot.Services;
public class TextFileHandler : IFileHandler
{
    private readonly AppSettings _appSettings;
    private readonly ITelegramBotClient _telegramBotClient;

    public TextFileHandler(ITelegramBotClient telegramBotClient, AppSettings appSettings)
    {
        _appSettings = appSettings;
        _telegramBotClient = telegramBotClient;
    }

    public async Task Download(string userText, CancellationToken ct)
    {
        //Полный путь файла из конфигурации
        string inputTextFilePath = Path.Combine(_appSettings.DownloadsFolder, "CountAndSumBot",
            $"{_appSettings.TextFileName}.{_appSettings.TextFormat}");

        Directory.CreateDirectory(Path.GetDirectoryName(inputTextFilePath) ?? string.Empty);

        if (!string.IsNullOrEmpty(userText))
        {
            await System.IO.File.WriteAllTextAsync(inputTextFilePath, userText, cancellationToken: ct);
        }
        else
        {
            throw new ArgumentException("Пользователь не ввел текст!");
        }
    }

    public string Process(string option)
    {
        string inputTextPath = Path.Combine(_appSettings.DownloadsFolder, "CountAndSumBot",
            $"{_appSettings.TextFileName}.{_appSettings.TextFormat}");

        Console.WriteLine("Начинаем конвертацию цифр...");
        var userNumbers = NumberConverter.TryConvert(inputTextPath);

        Console.WriteLine("Суммируем цифры...");
        var numbersSum = SumNumbers.Sum(userNumbers); 
        return numbersSum;
    }
}
