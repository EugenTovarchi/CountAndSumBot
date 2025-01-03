﻿using CountAndSumBot.Configuration;
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

    public async Task<string> Handle(Message message, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(message.Text)) // Если сообщение содержит текст
        {
            try
            { 
                await _textFileHandler.Download(message.Text, ct);
                string userOption = _memoryStorage.GetSession(message.Chat.Id).Option;
                string result = _textFileHandler.Process(userOption);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке данных: {ex.Message}");
                return $"{ex.Message}";
            }
        }
        return "Пожалуйста, отправьте текст с цифрами.";
    }
}
