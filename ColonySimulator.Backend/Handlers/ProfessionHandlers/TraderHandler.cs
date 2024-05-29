using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class TraderHandler : ITraderHandler
{
    public Task Trade(Crops crops, Wood wood, Medicine medicine, Herbs herbs, Weaponry weaponry)
    {
        var resources = new List<String>();
        resources.Add("Crops");
        resources.Add("Wood");
        resources.Add("Medicine");
        resources.Add("Herbs");
        resources.Add("Weaponry");

        var resourcesCount = new List<int>();
        resourcesCount.Add(crops.CropsCount);
        resourcesCount.Add(wood.WoodCount);
        resourcesCount.Add(medicine.MedicineCount);
        resourcesCount.Add(herbs.HerbsCount);
        resourcesCount.Add(weaponry.WeaponryCount);

        var indexMax = resourcesCount.IndexOf(resourcesCount.Max());
        var indexMin = resourcesCount.IndexOf(resourcesCount.Min());

        Console.WriteLine("You can trade: \n" + resources[indexMax] + " --> " + resources[indexMin] + "" + "\n" 
            + resourcesCount.Max() + " --> " + resourcesCount.Min());
        
        return Task.CompletedTask;
    }
    
    public Task ExperienceThreat(Threat threat)
    {
        throw new NotImplementedException();
    }
}