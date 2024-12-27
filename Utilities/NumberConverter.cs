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
            while (!sr.EndOfStream) 
            {
                string line = sr.ReadLine();
                string[] sNumbers = line.Split(' ');

                foreach (string sNumber in sNumbers)
                {
                    if (int.TryParse(sNumber, out int number))
                    {
                        numbers.Add(number); 
                    }
                    else
                    {
                        Console.WriteLine($"Некорректное значение: {sNumber}");
                        throw new Exception("Пожалуйста, отправьте текст с цифрами.");
                    }
                }
            }
        }
        return numbers;
    }
}
