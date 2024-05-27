using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Helpers;
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
    
    /// <summary>
    /// Constructor for this service
    /// </summary>
    /// <param name="serviceScopeFactory">Service scope factory</param>
    /// <param name="counter">Population counter class</param>
    /// <param name="year">current year in simulation</param>
    
    public StartSimulationService(IServiceScopeFactory serviceScopeFactory, PopCounter counter, Year year)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _counter = counter;
        _year = year;
    }
    
    /// <summary>
    /// Start simulation based on specified parameters and don't stop till everyone is dead
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    
    public async Task RunAsync(CancellationToken ct)
    {
        using var serviceScope = _serviceScopeFactory.CreateScope();
        var profHandler = serviceScope.ServiceProvider.GetService<IProfessionHandler>();

        if (profHandler is not null)
        {
            //for this case simulation will go till everyone in population is dead
            for (;;)
            {
                _year.YearOfSim++;
                await profHandler.HandleApothecary();
                await profHandler.HandleFarm();

                if (_counter.PopulationCount == 0)
                {
                    Console.WriteLine("Everyone's dead");
                    break;
                }
            }
        }
    }
}