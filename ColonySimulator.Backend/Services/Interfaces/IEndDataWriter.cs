namespace ColonySimulator.Backend.Services.Interfaces;

public interface IEndDataWriter
{
    Task WriteEndDataAsync(CancellationToken ct);
}