namespace ColonySimulator.Backend.Services.Interfaces;

/// <summary>
/// Writes end data to json file
/// </summary>
public interface IEndDataWriter
{
    /// <summary>
    /// Writes end data to json file
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    Task WriteEndDataAsync(CancellationToken ct);
}