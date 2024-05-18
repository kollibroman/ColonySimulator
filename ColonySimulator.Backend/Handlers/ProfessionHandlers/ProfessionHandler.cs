using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class ProfessionHandler : IProfessionHandler
{
    private readonly IFarmerHandler _farmerHandler;
    private readonly IApothecaryHandler _apothecaryHandler;
    private readonly IBlackSmithHandler _blackSmithHandler;
    private readonly IMedicHandler _medicHandler;
    private readonly ITimberHandler _timberHandler;
    private readonly ITraderHandler _traderHandler;


    public Task DoWork(Proffesion proffesion)
    {
        throw new NotImplementedException();
    }

    public Task ExperienceThreat(Effect effect)
    {
        throw new NotImplementedException();
    }

    public ProfessionHandler(IFarmerHandler farmerHandler, IApothecaryHandler apothecaryHandler, IBlackSmithHandler blackSmithHandler,
                                IMedicHandler medicHandler, ITimberHandler timberHandler, ITraderHandler traderHandler)
    {
        _farmerHandler = farmerHandler;
        _apothecaryHandler = apothecaryHandler;
        _blackSmithHandler = blackSmithHandler;
        _medicHandler = medicHandler;
        _timberHandler = timberHandler;
        _traderHandler = traderHandler;
    }

    public async Task HandleFarm()
    {
        throw new NotImplementedException();
    }

    public async Task HandleApothecary()
    {
        throw new NotImplementedException();
    }
    
    public async Task HandleBlackSmith()
    {
        throw new NotImplementedException();
    }
    
    public async Task HandleMedic()
    {
        throw new NotImplementedException();
    }
    
    public async Task HandleTimber()
    {
        throw new NotImplementedException();
    }
    
    public async Task HandleTrader()
    {
        throw new NotImplementedException();
    }
}