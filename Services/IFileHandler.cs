namespace CountAndSumBot.Services;

public interface IFileHandler
{
    Task Download(string fileId, string userText, CancellationToken ct);
    string Process(string option);
}
