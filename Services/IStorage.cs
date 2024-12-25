using CountAndSumBot.Models;

namespace CountAndSumBot.Services;

public interface IStorage
{
    /// <summary>
    /// Получение сессии пользователя по идентификатору
    /// </summary>
    /// <param name="chatId"></param>
    /// <returns></returns>
    Session GetSession(long chatId);
}