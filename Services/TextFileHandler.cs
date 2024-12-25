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

    public async Task Download(string fileId, CancellationToken ct)
    {
        // Генерируем полный путь файла из конфигурации
        string inputTextFilePath = Path.Combine(_appSettings.DownloadsFolder,"CountAndSumBot",
            $"{_appSettings.TextFileName}.{_appSettings.TextFormat}");

        using (FileStream destinationStream = System.IO.File.Create(inputTextFilePath))
        {
            // Загружаем информацию о файле
            var file = await _telegramBotClient.GetFile(fileId, ct);
            if (file.FilePath == null)
                return;

            // Скачиваем файл
            await _telegramBotClient.DownloadFile(file.FilePath, destinationStream, ct);
        }
    }

    public string Process(string option)
    {
        string inputTextPath = Path.Combine(_appSettings.DownloadsFolder, "CountAndSumBot", $"{_appSettings.TextFileName}.{_appSettings.TextFormat}");

        Console.WriteLine("Начинаем конвертацию цифр...");
        var userNumbers = NumberConverter.TryConvert(inputTextPath);//путь к файлу );
        Console.WriteLine("Текст успешно конвертирован в цифры.");

        Console.WriteLine("Суммируем цифры...");
        var numbersSum = SumNumbers.Sum(userNumbers);
        Console.WriteLine("Сумма определена");
        return numbersSum;
    }
}
