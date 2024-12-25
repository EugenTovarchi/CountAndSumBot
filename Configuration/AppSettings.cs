namespace CountAndSumBot.Configuration;

/// <summary>
/// Класс отвечает за хранение конфигураций и инициализируется данными при старте программы.
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Токен Telegram API
    /// </summary>
    public string BotToken { get; set; }
    /// <summary>
    /// Папка загрузки файлов
    /// </summary>
    public string DownloadsFolder { get; set; }
    /// <summary>
    /// Имя файла при загрузке
    /// </summary>
    public string TextFileName { get; set; }
    /// <summary>
    /// Формат текстового файла
    /// </summary>
    public string TextFormat { get; set; }
}
