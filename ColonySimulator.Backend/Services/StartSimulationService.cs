using ColonySimulator.Backend.Handlers.Interfaces;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Helpers.Interfaces;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace ColonySimulator.Backend.Services;

/// <summary>
/// Class to fire up simulation
/// </summary>
public class StartSimulationService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly PopCounter _counter;
    private readonly Year _year;
    private readonly IEntityManagementService _entityManagementService;
    private readonly IThreatProvider _threatProvider;
    
    /// <summary>
    /// Constructor for this service
    /// </summary>
    /// <param name="serviceScopeFactory">Service scope factory</param>
    /// <param name="counter">Population counter class</param>
    /// <param name="year">current year in simulation</param>
    /// <param name="entityManagementService">Entity lifecycle service</param>
    /// <param name="threatProvider">threat to provide</param>
    public StartSimulationService(IServiceScopeFactory serviceScopeFactory, PopCounter counter, Year year, IEntityManagementService entityManagementService, IThreatProvider threatProvider)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _counter = counter;
        _year = year;
        _entityManagementService = entityManagementService;
        _threatProvider = threatProvider;
    }

    /// <summary>
    /// Start simulation based on specified parameters and don't stop till everyone is dead
    /// </summary>
    /// <param name="ct">Cancellation token</param>

    public async Task RunAsync(bool isManually, CancellationToken ct)
    {
        using var serviceScope = _serviceScopeFactory.CreateScope();
        var profHandler = serviceScope.ServiceProvider.GetService<IProfessionHandler>();
        var resHandler = serviceScope.ServiceProvider.GetService<IResourceHandler>();
        var threatHandler = serviceScope.ServiceProvider.GetService<IThreatHandler>();
        var dbContext = serviceScope.ServiceProvider.GetService<ColonySimulatorContext>();

        int yearsToFinish;
        int yearsByUser;

        if (profHandler is not null && dbContext is not null && resHandler is not null && threatHandler is not null)
        {
            var rnd = new Random();

            if (isManually)
            {
                yearsToFinish = AnsiConsole.Prompt(new TextPrompt<int>("For how many years should simulation be running: ")
                    .ValidationErrorMessage("[red]Inproper input!![/]")
                    .Validate(yearsToFinish =>
                    {
                        return yearsToFinish switch
                        {
                            <= 0 => ValidationResult.Error("[red]You need at least one year of simulation[/]"),
                            >= 250 => ValidationResult.Error("[red]Don't exceed 250 years[/]"),
                            _ => ValidationResult.Success()
                        };
                    }));
            }
            
            else
            {
                yearsToFinish = _year.SimDuration;
            }
            
            Console.Clear();
            for (;;)
            {
                _year.YearOfSim++;
                
                await profHandler.HandleApothecary();
                await profHandler.HandleFarm();
                await profHandler.HandleTimber();
                await profHandler.HandleMedic();
                await profHandler.HandleBlackSmith();
                await resHandler.ConsumeResources(_counter.PopulationCount);

                _threatProvider.ThreatToExperience = null;
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

                var resourceOverview = new ResourceOverview
                {
                    CropsCount = dbContext.Crops.SingleOrDefault(x => x.Id == 1)!.CropsCount,
                    HerbsCount = dbContext.Herbs.SingleOrDefault(x => x.Id == 1)!.HerbsCount,
                    WeaponryCount = dbContext.Weaponry.SingleOrDefault(x => x.Id == 1)!.WeaponryCount,
                    MedicinesCount = dbContext.Medicines.SingleOrDefault(x => x.Id == 1)!.MedicineCount,
                    WoodCount = dbContext.Wood.SingleOrDefault(x => x.Id == 1)!.WoodCount
                };

                //Needs further improvement with new console lib in project
                //Console.WriteLine(_displayService.SerializeAndDisplayData<ProfessionsOverview, ThreatsOverview>(profOverview, threatOverview, resourceOverview))          
                /*################################## Layout GUI ##################################*/
                //Adding year rule on top of console
                var yearRule = new Panel("Year: [red]" + _year.YearOfSim + "[/]\n");

                //Population Counter Panel
                var popCountPanel = new Panel(
                    new Markup("Apothecaries: " + _counter.ApothecariesCount
                                                + "\nBlack smiths: " + _counter.BlackSmithCount + "\nFarmers: " +
                                                _counter.FarmerCount
                                                + "\nMedics: " + _counter.MedicCount + "\nTimbers: " +
                                                _counter.TimberCount + "\nSummary: " + (_counter.PopulationCount - 1))
                )
                {
                    Border = BoxBorder.Heavy,
                    Header = new PanelHeader(" Population "),
                    Width = 20
                };

                //Resources Panel
                var resourcePanel = new Panel(new BarChart()
                    .Width(150)
                    .AddItem("Crops: ", resourceOverview.CropsCount, Color.Green)
                    .AddItem("Herbs: ", resourceOverview.HerbsCount, Color.Green1)
                    .AddItem("Wood: ", resourceOverview.WoodCount, Color.RosyBrown)
                    .AddItem("Medicine: ", resourceOverview.MedicinesCount, Color.Aqua)
                    .AddItem("Weaponry: ", resourceOverview.WeaponryCount, Color.Red1)
                )
                {
                    Border = BoxBorder.Heavy,
                    Header = new PanelHeader(" Resources ")
                };
                resourcePanel.Padding(1, 1, 1, 1);

                //Threat Panel
                //Generate random threat and then handle it
                var threatName = "No threats this year";
                if (rnd.NextDouble() <= 0.2)
                {
                    var threat = await threatHandler.GenerateRandomThreat(ct);
                    
                    if (threat is not null)
                    {
                        await threatHandler.SetActiveThreat(threat, ct);
                        _threatProvider.ThreatToExperience = threat;
                        threatName = _threatProvider.ThreatToExperience.Name;
                    }
                }
                var threatPanel = new Panel(
                    new Markup(threatName)
                );
                threatPanel.Border = BoxBorder.Heavy;
                threatPanel.Header = new PanelHeader(" Threats: ");
                threatPanel.Width = 20;

                //Trader Panel
                //Generate Trader and handle it
                var traderString = "No trader this year";
                if (rnd.NextDouble() <= 0.2)
                {
                    var resourcesCount = new List<int>
                    {
                        resourceOverview.CropsCount,
                        resourceOverview.HerbsCount,
                        resourceOverview.MedicinesCount,
                        resourceOverview.WeaponryCount,
                        resourceOverview.WoodCount
                    };

                    var resourcesName = new List<String>
                    {
                        "Crops",
                        "Herbs",
                        "Medicine",
                        "Weaponry",
                        "Wood"
                    };

                    var indexMax = resourcesCount.IndexOf(resourcesCount.Max());
                    var indexMin = resourcesCount.IndexOf(resourcesCount.Min());
                    var amountTraded = (int)Math.Ceiling(resourcesCount[indexMax] * 0.7);

                    traderString = resourcesName[indexMax] + " --> " + resourcesName[indexMin] + "\n"
                                   + resourcesCount[indexMax] + " --> " + resourcesCount[indexMin] +
                                   "\nAmount traded: " + amountTraded;

                    await profHandler.HandleTrader();
                }

                var traderPanel = new Panel(
                    new Markup(traderString)
                )
                {
                    Border = BoxBorder.Heavy,
                    Header = new PanelHeader("Trader: ")
                };

                //Layout
                var layout = new Layout("Root")
                    .SplitRows(new Layout("Top")
                            .SplitColumns(new Layout("TopL"), new Layout("TopM"), new Layout("TopR")
                                .SplitRows(new Layout("TopRT"), new Layout("TopRB"))), new Layout("Bottom")
                    );


                layout["TopL"].Update(yearRule).Size(12);

                layout["TopM"].Update(popCountPanel).Size(20);

                layout["TopR"].Size(30);

                layout["TopRT"].Update(threatPanel).Ratio(1);

                layout["TopRB"].Update(traderPanel).Ratio(2);

                layout["Bottom"].Update(resourcePanel);

                Console.Clear();
                AnsiConsole.Write(layout);
                
                await _entityManagementService.CleanupDeadEntities(ct);
                await _entityManagementService.GenerateNewEntity(resourceOverview.CropsCount, ct);
                await _entityManagementService.CheckHungerStatus(ct);
                await _entityManagementService.CheckSickStatus(ct);

                if (_counter.PopulationCount == 0)
                {
                    Console.WriteLine("Everyone's dead, showing end data: ");
                    break;
                }

                if (_year.YearOfSim == yearsToFinish)
                {
                    break;
                }

                if (traderString != "No trader this year")
                {
                    Thread.Sleep(500);
                }
                else
                {
                    Thread.Sleep(200);
                }
            }
        }
    }
}