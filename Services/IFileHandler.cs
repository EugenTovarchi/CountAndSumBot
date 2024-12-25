namespace CountAndSumBot.Services;

public interface IFileHandler
{
    Task Download(string fileId, CancellationToken ct);
    string Process(string option);
}
