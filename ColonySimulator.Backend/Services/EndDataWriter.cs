using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ColonySimulator.Backend.Services;

/// <summary>
/// Writes ending data to json file
/// </summary>
public class EndDataWriter : IEndDataWriter
{
    private readonly EndDataStorer _dataStorer;
    private readonly RandomSeedingData _randomSeedingData;
    
    /// <summary>
    /// Constructor for it with DI parameters
    /// </summary>
    /// <param name="dataStorer">Data storer class</param>
    /// <param name="randomSeedingData">Seeding data storer class</param>
    public EndDataWriter(EndDataStorer dataStorer, RandomSeedingData randomSeedingData)
    {
        _dataStorer = dataStorer;
        _randomSeedingData = randomSeedingData;
    }
    
    /// <summary>
    /// Writes End data to json file
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    public async Task WriteEndDataAsync(CancellationToken ct)
    {
        var serializedString = JsonConvert.SerializeObject(_dataStorer, Formatting.Indented);
        await File.WriteAllTextAsync("EndData.json", serializedString, ct);

        if (_randomSeedingData.Duration != 0)
        {
            var serializedData = JsonConvert.SerializeObject(_randomSeedingData, Formatting.Indented);
            await File.WriteAllTextAsync("RandomStartData.json", serializedData, ct);
        }
    }
}