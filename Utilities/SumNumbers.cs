namespace CountAndSumBot.Utilities;

/// <summary>
/// Класс выполняет суммирование цифр из текста пользователя.
/// </summary>
public static class SumNumbers
{
    // вывести на .ToString();
    public static string Sum(List<int> numbers)
    {
        int result = numbers.Sum(x => x);
        return $"Сумма ваших чисел: {result}";
    }
}