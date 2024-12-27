namespace CountAndSumBot.Services;

public interface IFileHandler
{
    Task Download(string userText, CancellationToken ct);
    string Process(string option);
}
