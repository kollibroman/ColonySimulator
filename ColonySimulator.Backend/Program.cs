using ColonySimulator.Backend.Handlers;
using ColonySimulator.Backend.Handlers.Interfaces;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Handlers.ProfessionHandlers;
using ColonySimulator.Backend.Handlers.ResourceHandlers;
using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Helpers.Interfaces;
using ColonySimulator.Backend.Services;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Seeders;
using ColonySimulator.Backend.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ColonySimulator.Backend;

/// <summary>
/// Main entry point of simulation
/// </summary>
public class Program
{
    /// <summary>
    /// Main entry point of program
    /// </summary>
    /// <param name="args">CLI arguments</param>
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);
        builder.ConfigureAppConfiguration(x =>
        {
            x.AddJsonFile("./appsettings.json", optional: true, reloadOnChange: true);
        })
        .ConfigureServices((config, services) =>
        {
            services.AddHostedService<StartupService>();
            var connectionString = config.Configuration.GetConnectionString("BasicDb");

            services.AddScoped<ProfessionSeeder>();
            services.AddScoped<ResourceSeeder>();
            services.AddScoped<ThreatSeeder>();
            services.AddScoped<DataSeeder>();
            
            services.AddScoped<IApothecaryHandler, ApothecaryHandler>();
            services.AddScoped<IBlackSmithHandler, BlackSmithHandler>();
            services.AddScoped<IFarmerHandler, FarmerHandler>();
            services.AddScoped<IMedicHandler, MedicHandler>();
            services.AddScoped<ITimberHandler, TimberHandler>();
            services.AddScoped<ITraderHandler, TraderHandler>();
            services.AddScoped<IResourceHandler, ResourceHandler>();
            services.AddScoped<IThreatHandler, ThreatHandler>();

            services.AddScoped<IProfessionHandler, ProfessionHandler>();
            
            services.AddSingleton<Year>();
            services.AddSingleton<PopCounter>();
            services.AddSingleton<StartupService>();
            services.AddSingleton<StartSimulationService>();
            services.AddSingleton<IThreatProvider, ThreatProvider>();
            services.AddSingleton<IEntityManagementService, EntityManagementService>();
            services.AddSingleton<EndDataStorer>();
            services.AddSingleton<RandomSeedingData>();
            services.AddSingleton<IEndDataWriter, EndDataWriter>();
            
            services.AddSqlite<ColonySimulatorContext>(connectionString);
        })
        .ConfigureLogging(x =>
        {
            x.AddSerilog();
        })
        .UseConsoleLifetime()
        .UseSerilog((builderContext, configuration) =>
            configuration.ReadFrom.Configuration(builderContext.Configuration));

        var host = builder.Build();
        
        await host.RunAsync();
    }
}