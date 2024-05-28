using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
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
        var dbContext = serviceScope.ServiceProvider.GetService<ColonySimulatorContext>();

        if (profHandler is not null && dbContext is not null)
        {
            //for this case simulation will go till everyone in population is dead
            //but for testing purposes, it will stop after 10 years
            for (;;)
            {
                _year.YearOfSim++;
                await profHandler.HandleApothecary();
                await profHandler.HandleFarm();

                if (_year.YearOfSim == 10)
                {
                    Console.WriteLine("Simulation end, specified period timed out! Showing data: ");
                    
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
                
                    //Needs further improvement with new console lib in project
                    Console.WriteLine(_displayService.SerializeAndDisplayData<ProfessionsOverview, ThreatsOverview>(profOverview, threatOverview));
                    break;
                }
                
                if (_counter.PopulationCount == 0)
                {
                    Console.WriteLine("Everyone's dead, showing end data: ");
                    break;
                }
            }
        }
    }
}