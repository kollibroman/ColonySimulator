using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ColonySimulator.Backend.Services;

public class EndDataWriter : IEndDataWriter
{
    private readonly EndDataStorer _dataStorer;
    private readonly RandomSeedingData _randomSeedingData;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly PopCounter _counter;

    public EndDataWriter(EndDataStorer dataStorer, RandomSeedingData randomSeedingData,
        IServiceScopeFactory serviceScopeFactory, PopCounter counter)
    {
        _dataStorer = dataStorer;
        _randomSeedingData = randomSeedingData;
        _serviceScopeFactory = serviceScopeFactory;
        _counter = counter;
    }
    
    public async Task WriteEndDataAsync(CancellationToken ct)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ColonySimulatorContext>();
        
        var popCount = await dbContext.Apothecaries.CountAsync(ct) + await dbContext.Traders.CountAsync(ct) +
                       await dbContext.Medics.CountAsync(ct) + await dbContext.Farmers.CountAsync(ct) +
                       await dbContext.BlackSmiths.CountAsync(ct) + await dbContext.Timbers.CountAsync(ct);
        
        _dataStorer.PopulationCount = popCount;

        _dataStorer.ProfessionsOverview.Apothecaries = await dbContext.Apothecaries.ToListAsync(ct);
        _dataStorer.ProfessionsOverview.Traders = await dbContext.Traders.ToListAsync(ct);
        _dataStorer.ProfessionsOverview.Medics = await dbContext.Medics.ToListAsync(ct);
        _dataStorer.ProfessionsOverview.Farmers = await dbContext.Farmers.ToListAsync(ct);
        _dataStorer.ProfessionsOverview.BlackSmiths = await dbContext.BlackSmiths.ToListAsync(ct);
        _dataStorer.ProfessionsOverview.Timbers = await dbContext.Timbers.ToListAsync(ct);
        
        _dataStorer.ResourceOverview.CropsCount = await dbContext.Crops.CountAsync(ct);
        _dataStorer.ResourceOverview.HerbsCount = await dbContext.Herbs.CountAsync(ct);
        _dataStorer.ResourceOverview.WoodCount = await dbContext.Wood.CountAsync(ct);
        _dataStorer.ResourceOverview.WeaponryCount = await dbContext.Weaponry.CountAsync(ct);
        _dataStorer.ResourceOverview.MedicinesCount = await dbContext.Medicines.CountAsync(ct);
        
        var serializedString = JsonConvert.SerializeObject(_dataStorer, Formatting.Indented);
        await File.WriteAllTextAsync("EndData.json", serializedString, ct);

        if (_randomSeedingData.Duration != 0)
        {
            var serializedData = JsonConvert.SerializeObject(_randomSeedingData, Formatting.Indented);
            await File.WriteAllTextAsync("RandomStartData.json", serializedData, ct);
        }
    }
}