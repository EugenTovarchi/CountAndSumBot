namespace CountAndSumBot.Utilities;

/// <summary>
/// Конвертируем текст в цифры для Option: Sum
/// </summary>
public static class NumberConverter
{
    public static List<double> TryConvert(string path)
    {
        List<double> numbers = new List<double>();

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
                    if (double.TryParse(sNumber, out double number))
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
