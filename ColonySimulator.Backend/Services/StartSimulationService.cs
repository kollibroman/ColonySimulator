using ColonySimulator.Backend.Handlers.Interfaces;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ColonySimulator.Backend.Services;

/// <summary>
/// Class to fire up simulation
/// </summary>

public class StartSimulationService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly PopCounter _counter;
    private readonly Year _year;
    private readonly DataDisplayService _displayService;
    
    /// <summary>
    /// Constructor for this service
    /// </summary>
    /// <param name="serviceScopeFactory">Service scope factory</param>
    /// <param name="counter">Population counter class</param>
    /// <param name="year">current year in simulation</param>
    /// <param name="displayService">Data display service</param>
    
    public StartSimulationService(IServiceScopeFactory serviceScopeFactory, PopCounter counter, Year year, DataDisplayService displayService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _counter = counter;
        _year = year;
        _displayService = displayService;
    }
    
    /// <summary>
    /// Start simulation based on specified parameters and don't stop till everyone is dead
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    
    public async Task RunAsync(CancellationToken ct)
    {
        using var serviceScope = _serviceScopeFactory.CreateScope();
        var profHandler = serviceScope.ServiceProvider.GetService<IProfessionHandler>();
        var resHandler = serviceScope.ServiceProvider.GetService<IResourceHandler>();
        var dbContext = serviceScope.ServiceProvider.GetService<ColonySimulatorContext>();
        
        Console.WriteLine("Starting simulation...");
        
        if (profHandler is not null && dbContext is not null && resHandler is not null)
        {
            //for this case simulation will go till everyone in population is dead
            //but for testing purposes, it will stop after 10 years
            for (;;)
            {
                _year.YearOfSim++;
                await profHandler.HandleApothecary();
                await profHandler.HandleFarm();
                await profHandler.HandleTimber();
                await profHandler.HandleMedic();
                await profHandler.HandleBlackSmith();
                await resHandler.ConsumeResources(_counter.PopulationCount);

                //For now task getting once
                //I have cool idea for it now not really working
                //for now please add only 1 trader per sim
                //Basic display works changing data doesn't
                if (_year.YearOfSim % 10 == 0 )
                {
                    await profHandler.HandleTrader();
                }
                
                    //Console.WriteLine("Simulation end, specified period timed out! Showing data: ");
                    
                    var profOverview = new ProfessionsOverview
                    {
                        Apothecaries = await dbContext.Apothecaries.ToListAsync(ct),
                        BlackSmiths = await dbContext.BlackSmiths.ToListAsync(ct),
                        Timbers = await dbContext.Timbers.ToListAsync(ct),
                        Traders = await dbContext.Traders.ToListAsync(ct),
                        Medics = await dbContext.Medics.ToListAsync(ct),
                        Farmers = await dbContext.Farmers.ToListAsync(ct),
                    };
        
                    //change to some reliable values later
                    var threatOverview = new ThreatsOverview
                    {
                        ThreatsDefeated = await dbContext.PlagueThreats.Where(x => x.Id % 2 == 0).ToListAsync(ct),
                        ThreatsYieldedTo = await dbContext.PlagueThreats.Where(x => x.Id % 2 != 0).ToListAsync(ct),
                    };

                    var resourceOverview = new ResourceOverview
                    {
                        CropsCount = dbContext.Crops.SingleOrDefault(x => x.Id == 1).CropsCount,
                        HerbsCount = dbContext.Herbs.SingleOrDefault(x => x.Id == 1).HerbsCount,
                        WeaponryCount = dbContext.Weaponry.SingleOrDefault(x => x.Id == 1).WeaponryCount,
                        MedicinesCount = dbContext.Medicines.SingleOrDefault(x => x.Id == 1).MedicineCount,
                        WoodCount = dbContext.Wood.SingleOrDefault(x => x.Id == 1).WoodCount
                    };
                
                    //Needs further improvement with new console lib in project
                    //Console.WriteLine(_displayService.SerializeAndDisplayData<ProfessionsOverview, ThreatsOverview>(profOverview, threatOverview, resourceOverview));
                    
                    Console.WriteLine("Year: " + _year.YearOfSim);
                    Console.WriteLine("Crops: " + resourceOverview.CropsCount);
                    Console.WriteLine("Herbs: " + resourceOverview.HerbsCount);
                    Console.WriteLine("Wood: " + resourceOverview.WoodCount);
                    Console.WriteLine("Weapons: " + resourceOverview.WeaponryCount);
                    Console.WriteLine("Medicine: " + resourceOverview.MedicinesCount);
                    Console.WriteLine("PopulationCount: " + _counter.PopulationCount);
                    Console.WriteLine("Threats count: " + threatOverview.ThreatsDefeated.Count);
                    Console.WriteLine("\n");

                    if (_year.YearOfSim == 10) break;
                    
                    
                if (_counter.PopulationCount == 0)
                {
                    Console.WriteLine("Everyone's dead, showing end data: ");
                    break;
                }
            }
        }
    }
}