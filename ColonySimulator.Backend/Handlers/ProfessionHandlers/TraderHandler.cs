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

        int willingToTrade, amountSold;

        try
        {
            Console.WriteLine("If you are willing to trade (type: 1), if not (type: 0): ");
            willingToTrade = int.Parse(Console.ReadLine()!);
            if (willingToTrade == 1)
            {
                Console.WriteLine("How much you want to trade: ");
                amountSold = int.Parse(Console.ReadLine()!);
                if (amountSold > resourcesCount.Max() || amountSold < 0)
                {
                    //Needs adding some sort of loop to get user input again 
                    Console.WriteLine("Can't trade this amount of resource");
                }
                else
                {
                    //Kinda works but doesn't display data in the servicesimstart
                    Console.WriteLine("Max before: " + resourcesCount[indexMax]);
                    resourcesCount[indexMax] -= amountSold;
                    Console.WriteLine("Max after: " + resourcesCount[indexMax]);
                    Console.WriteLine("Min before: " + resourcesCount[indexMin]);
                    resourcesCount[indexMin] += amountSold;
                    Console.WriteLine("Min after: " + resourcesCount[indexMin]);
                }
            }
            else
            {
                Console.WriteLine("Transaction rejected");
                return Task.CompletedTask;
            }
        }
        //Need to add logger I think
        catch (ArgumentNullException ex)
        {
            
        }
        
        
        return Task.CompletedTask;
    }
    
    public Task ExperienceThreat(Threat threat)
    {
        throw new NotImplementedException();
    }
}