namespace CountAndSumBot.Utilities;

/// <summary>
/// Конвертируем текст в цифры для Option: Sum
/// </summary>
public static class NumberConverter
{
    public static List<int> TryConvert(string path)
    {
        List<int> numbers = new List<int>();

        using (StreamReader sr = System.IO.File.OpenText(path))
        {
            while (!sr.EndOfStream) // Пока файл не закончился
            {
                // Читаем строку
                string line = sr.ReadLine();

                // Разделяем строку на элементы
                string[] sNumbers = line.Split(' ');

                // Конвертируем элементы в числа
                foreach (string sNumber in sNumbers)
                {
                    if (int.TryParse(sNumber, out int number))
                    {
                        numbers.Add(number); // Добавляем корректное число
                    }
                    else
                    {
                        Console.WriteLine($"Некорректное значение: {sNumber}");
                    }
                }
            }
        }
        return numbers;
    }
}
