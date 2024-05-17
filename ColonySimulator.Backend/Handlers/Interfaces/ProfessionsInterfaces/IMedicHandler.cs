using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface IMedicHandler
{
    public Task Heal(Medicine medicine);
}