using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using CountAndSumBot.Controllers;
using CountAndSumBot.Services;
using CountAndSumBot.Configuration;
using CountAndSumBot.Extensions;

namespace CountAndSumBot;

public class Program
{
    public static async Task Main()
    {
        Console.OutputEncoding = Encoding.Unicode;

        // Объект, отвечающий за постоянный жизненный цикл приложения
        var host = new HostBuilder()
            .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
            .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
            .Build(); // Собираем

        Console.WriteLine("Сервис запущен");
        // Запускаем сервис
        await host.RunAsync();
        Console.WriteLine("Сервис остановлен");
    }
    static AppSettings BuildAppSettings()
    {
        return new AppSettings()
        {
            DownloadsFolder = DirectoryExtension.GetSolutionRoot(),
            BotToken = "7859574653:AAH4Gv4f6YuNddbzLb47Az996fc5UXMes5g",
            TextFileName = "botFile",
            TextFormat = "txt"
        };
    }
    /// <summary>
    /// Контейнер зависимостей. Тут все компоненты становятся достпуными приложению.
    /// </summary>
    /// <param name="services"></param>
    static void ConfigureServices(IServiceCollection services)
    {
        AppSettings appSettings = BuildAppSettings();
        services.AddSingleton(appSettings);

        services.AddSingleton<IStorage, MemoryStorage>();
        services.AddSingleton<IFileHandler, TextFileHandler>();

        // Подключаем контроллеры сообщений и кнопок
        services.AddTransient<DefaultMessageController>();
        services.AddTransient<TextMessageController>();
        services.AddTransient<InlineKeyboardController>();
        services.AddTransient<LengthController>();
        services.AddTransient<SumController>();

        services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
        services.AddHostedService<Bot>();
    }
}