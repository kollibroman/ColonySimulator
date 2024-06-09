using System.Text;
using ColonySimulator.Backend.Handlers.Interfaces;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using Microsoft.EntityFrameworkCore;
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
    private readonly DataDisplayService _displayService;
    private readonly ThreatProvider _threatProvider;
    
    /// <summary>
    /// Constructor for this service
    /// </summary>
    /// <param name="serviceScopeFactory">Service scope factory</param>
    /// <param name="counter">Population counter class</param>
    /// <param name="year">current year in simulation</param>
    /// <param name="displayService">Data display service</param>
    /// <param name="threatProvider">threat to provide</param>
    public StartSimulationService(IServiceScopeFactory serviceScopeFactory, PopCounter counter, Year year, DataDisplayService displayService, ThreatProvider threatProvider)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _counter = counter;
        _year = year;
        _displayService = displayService;
        _threatProvider = threatProvider;
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
        var threatHandler = serviceScope.ServiceProvider.GetService<IThreatHandler>();
        var dbContext = serviceScope.ServiceProvider.GetService<ColonySimulatorContext>();
        
        Console.WriteLine("Starting simulation...");
        
        if (profHandler is not null && dbContext is not null && resHandler is not null && threatHandler is not null)
        {
            var rnd = new Random();
            //for this case simulation will go till everyone in population is dead
            //but for testing purposes, it will stop after 10 years
            
            Console.Clear();
            for (;;)
            {
                _year.YearOfSim++;
                
                //Generate random threat and then handle it
                var threatName = "No threats this year";
                if (rnd.NextDouble() <= 0.1)
                {
                    var threat = await threatHandler.GenerateRandomThreat(ct);

                    if (threat is not null)
                    {
                        _threatProvider.ThreatToExperience = threat;
                        threatName = _threatProvider.ThreatToExperience.Name;
                    }
                }
                
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
                var yearRule = new Rule("Year: [red]" + _year.YearOfSim + "[/]\n");

                //Population Counter Panel
                var popCountPanel = new Panel(
                    new Markup("Apothecaries: " + dbContext.Apothecaries.Count()
                        + "\nBlack smiths: " + dbContext.BlackSmiths.Count() + "\nFarmers: " + dbContext.Farmers.Count()
                        + "\nMedics: " + dbContext.Medics.Count() + "\nTimbers: " + dbContext.Timbers.Count() + "\nSummary: "+ _counter.PopulationCount)
                );
                popCountPanel.Border = BoxBorder.Heavy;
                popCountPanel.Header = new PanelHeader(" Population ");
                popCountPanel.Width = 20;
                
                //Resources Panel
                var resourcePanel = new Panel(new BarChart()
                    .Width(150)
                    .AddItem("Crops: ", resourceOverview.CropsCount, Color.Green)
                    .AddItem("Herbs: ", resourceOverview.HerbsCount, Color.Green1)
                    .AddItem("Wood: ", resourceOverview.WoodCount, Color.RosyBrown)
                    .AddItem("Medicine: ", resourceOverview.MedicinesCount, Color.Aqua)
                    .AddItem("Weaponry: ", resourceOverview.WeaponryCount, Color.Red1)
                );
                resourcePanel.Border = BoxBorder.Heavy;
                resourcePanel.Header = new PanelHeader(" Resources ");
                resourcePanel.Padding(1, 1, 1, 1);
            
                //Threat Panel
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
                        + resourcesCount[indexMax] + " --> " + resourcesCount[indexMin] + "\nAmount traded: " + amountTraded;

                    await profHandler.HandleTrader();
                }

                var traderPanel = new Panel(
                    new Markup(traderString)
                );
                traderPanel.Border = BoxBorder.Heavy;
                traderPanel.Header = new PanelHeader("Trader: ");
                
                //Layout
                var layout = new Layout("Root")
                    .SplitRows(
                        new Layout("Top")
                            .SplitRows(
                                new Layout("TopT"),
                                new Layout("BotT")
                                    .SplitColumns(
                                        new Layout("Left"),
                                        new Layout("Right")
                                            .SplitRows(new Layout("RTop"), new Layout("RBot"))
                                    )
                            ),
                        new Layout("Bottom")
                    );
                
                layout["TopT"].Update(yearRule).Ratio(1);

                layout["BotT"].Ratio(10);
                
                layout["Left"].Update(popCountPanel).Size(20);
                
                layout["Right"].MinimumSize(20);

                layout["Rtop"].Update(threatPanel).Ratio(1);

                layout["RBot"].Update(traderPanel).Ratio(2);
                
                layout["Bottom"].Update(resourcePanel);
                
                Console.Clear();
                AnsiConsole.Write(layout);
                
                if (_year.YearOfSim == 10) break;
                    
                if (_counter.PopulationCount == 0)
                {
                    Console.WriteLine("Everyone's dead, showing end data: ");
                    break;
                }

                Console.ReadLine();
            }
        }
    }
}