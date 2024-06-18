using System.Linq.Expressions;
using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Handlers.ProfessionHandlers;
using ColonySimulator.Backend.Helpers.Interfaces;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;
using ColonySimulator.tests.TestAsyncQuery;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ColonySimulator.tests.Handlers.ProfessionHandlers;

[TestSubject(typeof(ProfessionHandler))]
public class ProfessionHandlerTest
{
    private DbContextOptions<ColonySimulatorContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<ColonySimulatorContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }
    
   [Fact]
    public async Task HandleFarmTest()
    {
        // Arrange
        var farmers = new List<Farmer>
        {
            new Farmer
            {
                Id = 1,
                Agility = 3,
            }, 
            new Farmer
            {
                Id = 2,
                Agility = 3
            }
        };
        
        var medics = new List<Medic>
        {
            new Medic
            {
                Id = 1,
                Agility = 3,
            }, 
            new Medic
            {
                Id = 2,
                Agility = 3,
            }
        };
        
        var blackSmiths = new List<BlackSmith>
        {
            new BlackSmith
            {
                Id = 1,
                Agility = 3,
            }, 
            new BlackSmith
            {
                Id = 2,
                Agility = 3,
                IsSick = true
            }
        };
        
        var apothecaries = new List<Apothecary>
        {
            new Apothecary
            {
                Id = 1,
                Agility = 3,
            }, 
            new Apothecary
            {
                Id = 2,
                Agility = 3
            }
        };
        
        var traders = new List<Trader>
        {
            new Trader
            {
                Id = 1,
                Agility = 3,
            }, 
        };
        
        var timbers = new List<Timber>
        {
            new Timber
            {
                Id = 1,
                Agility = 3,
            }, 
            new Timber
            {
                Id = 2,
                Agility = 3
            }
        };
        
        var crops = new Crops
        {
            Id = 1,
            Name = "wheat"
        };
        var herbs = new Herbs
        {
            Id = 1,
            Name = "peppermint"
        };
        
        var wood = new Wood
        {
            Id = 1,
            Name = "Oak",
            WoodCount = 10
        };
        
        var medicine = new Medicine
        {
            Id = 1,
            Name = "Aspirin",
            MedicineCount = 10
        };
        
        var weaponry = new Weaponry
        {
            Id = 1,
            Name = "Sword",
            WeaponryCount = 10
        };
        
        var resources = new List<Resource>
        {
            crops,
            herbs,
            wood,
            medicine,
            weaponry
        };

        var fightingEffect = new FightingThreatEffect
        {
            Damage = 0,
            Name = "Army",
            ResourcesStolen = resources
        };

        var mockFarmerHandler = new Mock<IFarmerHandler>();
        mockFarmerHandler.Setup(fh => fh.Farm(It.IsAny<Crops>(), It.IsAny<Herbs>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        mockFarmerHandler.Setup(fh => fh.ExperienceThreat(It.IsAny<Effect>(), It.IsAny<Farmer>(), It.IsAny<List<Resource>>())).Returns(Task.CompletedTask);

        var mockThreatHandler = new Mock<IThreatHandler>();
        mockThreatHandler.Setup(th => th.CalculateUsedResources(It.IsAny<List<Resource>>(), It.IsAny<Threat>())).Returns(resources);
        mockThreatHandler.Setup(th => th.CalculateAffection(It.IsAny<Proffesion>(), It.IsAny<Threat>())).Returns(Task.CompletedTask);
        mockThreatHandler.Setup(th => th.GenerateEffects(It.IsAny<Threat>(), It.IsAny<List<Resource>>())).ReturnsAsync(fightingEffect);
        
        var mockMedicHandler = new Mock<IMedicHandler>();
        mockMedicHandler.Setup(mh => mh.Heal(It.IsAny<Medicine>(), It.IsAny<Person>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        mockMedicHandler.Setup(mh => mh.ExperienceThreat(It.IsAny<Effect>(), It.IsAny<Medic>(), It.IsAny<List<Resource>>())).Returns(Task.CompletedTask);

        var mockBlackSmithHandler = new Mock<IBlackSmithHandler>();
        mockBlackSmithHandler.Setup(bsh => bsh.CreateWeapon(It.IsAny<Weaponry>(), It.IsAny<Wood>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        mockBlackSmithHandler.Setup(bsh => bsh.ExperienceThreat(It.IsAny<Effect>(), It.IsAny<BlackSmith>(), It.IsAny<List<Resource>>())).Returns(Task.CompletedTask);

        var mockApothecaryHandler = new Mock<IApothecaryHandler>();
        mockApothecaryHandler.Setup(ah => ah.CollectingHerbs(It.IsAny<Herbs>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        mockApothecaryHandler.Setup(ah => ah.CreateMedicine(It.IsAny<Herbs>(), It.IsAny<Medicine>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        mockApothecaryHandler.Setup(ah => ah.ExperienceThreat(It.IsAny<Effect>(), It.IsAny<Apothecary>(), It.IsAny<List<Resource>>())).Returns(Task.CompletedTask);

        var mockTraderHandler = new Mock<ITraderHandler>();
        mockTraderHandler.Setup(th => th.Trade(It.IsAny<Crops>(), It.IsAny<Wood>(), It.IsAny<Medicine>(), It.IsAny<Herbs>(), It.IsAny<Weaponry>())).Returns(Task.CompletedTask);
        mockTraderHandler.Setup(th => th.ExperienceThreat(It.IsAny<Effect>(), It.IsAny<Trader>(), It.IsAny<List<Resource>>())).Returns(Task.CompletedTask);

        var mockTimberHandler = new Mock<ITimberHandler>();
        mockTimberHandler.Setup(th => th.CreateWood(It.IsAny<Wood>(), It.IsAny<int>())).Returns(Task.CompletedTask);
        mockTimberHandler.Setup(th => th.ExperienceThreat(It.IsAny<Effect>(), It.IsAny<Timber>(), It.IsAny<List<Resource>>())).Returns(Task.CompletedTask);

        var mockDbSetFarmers = new Mock<DbSet<Farmer>>();
        mockDbSetFarmers.As<IAsyncEnumerable<Farmer>>().Setup(m => m.GetAsyncEnumerator(new CancellationToken())).Returns(new TestAsyncEnumerator<Farmer>(farmers.GetEnumerator()));
        mockDbSetFarmers.As<IQueryable<Farmer>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Farmer>(farmers.AsQueryable().Provider));
        mockDbSetFarmers.As<IQueryable<Farmer>>().Setup(m => m.Expression).Returns(farmers.AsQueryable().Expression);
        mockDbSetFarmers.As<IQueryable<Farmer>>().Setup(m => m.ElementType).Returns(farmers.AsQueryable().ElementType);
        mockDbSetFarmers.As<IQueryable<Farmer>>().Setup(m => m.GetEnumerator()).Returns(() => farmers.GetEnumerator());
        
        var mockDbSetMedics = new Mock<DbSet<Medic>>();
        mockDbSetMedics.As<IAsyncEnumerable<Medic>>().Setup(m => m.GetAsyncEnumerator(new CancellationToken())).Returns(new TestAsyncEnumerator<Medic>(medics.GetEnumerator()));
        mockDbSetMedics.As<IQueryable<Medic>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Medic>(medics.AsQueryable().Provider));
        mockDbSetMedics.As<IQueryable<Medic>>().Setup(m => m.Expression).Returns(medics.AsQueryable().Expression);
        mockDbSetMedics.As<IQueryable<Medic>>().Setup(m => m.ElementType).Returns(medics.AsQueryable().ElementType);
        mockDbSetMedics.As<IQueryable<Medic>>().Setup(m => m.GetEnumerator()).Returns(() => medics.GetEnumerator());
        
        var mockDbSetApothecaries = new Mock<DbSet<Apothecary>>();
        mockDbSetApothecaries.As<IAsyncEnumerable<Apothecary>>().Setup(m => m.GetAsyncEnumerator(new CancellationToken())).Returns(new TestAsyncEnumerator<Apothecary>(apothecaries.GetEnumerator()));
        mockDbSetApothecaries.As<IQueryable<Apothecary>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Apothecary>(apothecaries.AsQueryable().Provider));
        mockDbSetApothecaries.As<IQueryable<Apothecary>>().Setup(m => m.Expression).Returns(apothecaries.AsQueryable().Expression);
        mockDbSetApothecaries.As<IQueryable<Apothecary>>().Setup(m => m.ElementType).Returns(apothecaries.AsQueryable().ElementType);
        mockDbSetApothecaries.As<IQueryable<Apothecary>>().Setup(m => m.GetEnumerator()).Returns(() => apothecaries.GetEnumerator());
        
        var mockDbSetBlacksmiths = new Mock<DbSet<BlackSmith>>();
        mockDbSetBlacksmiths.As<IAsyncEnumerable<BlackSmith>>().Setup(m => m.GetAsyncEnumerator(new CancellationToken())).Returns(new TestAsyncEnumerator<BlackSmith>(blackSmiths.GetEnumerator()));
        mockDbSetBlacksmiths.As<IQueryable<BlackSmith>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<BlackSmith>(blackSmiths.AsQueryable().Provider));
        mockDbSetBlacksmiths.As<IQueryable<BlackSmith>>().Setup(m => m.Expression).Returns(blackSmiths.AsQueryable().Expression);
        mockDbSetBlacksmiths.As<IQueryable<BlackSmith>>().Setup(m => m.ElementType).Returns(blackSmiths.AsQueryable().ElementType);
        mockDbSetBlacksmiths.As<IQueryable<BlackSmith>>().Setup(m => m.GetEnumerator()).Returns(() => blackSmiths.GetEnumerator());
        
        var mockDbSetTraders = new Mock<DbSet<Trader>>();
        mockDbSetTraders.As<IAsyncEnumerable<Trader>>().Setup(m => m.GetAsyncEnumerator(new CancellationToken())).Returns(new TestAsyncEnumerator<Trader>(traders.GetEnumerator()));
        mockDbSetTraders.As<IQueryable<Trader>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Trader>(traders.AsQueryable().Provider));
        mockDbSetTraders.As<IQueryable<Trader>>().Setup(m => m.Expression).Returns(traders.AsQueryable().Expression);
        mockDbSetTraders.As<IQueryable<Trader>>().Setup(m => m.ElementType).Returns(traders.AsQueryable().ElementType);
        mockDbSetTraders.As<IQueryable<Trader>>().Setup(m => m.GetEnumerator()).Returns(() => traders.GetEnumerator());
        
        var mockDbSetTimbers = new Mock<DbSet<Timber>>();
        mockDbSetTimbers.As<IAsyncEnumerable<Timber>>().Setup(m => m.GetAsyncEnumerator(new CancellationToken())).Returns(new TestAsyncEnumerator<Timber>(timbers.GetEnumerator()));
        mockDbSetTimbers.As<IQueryable<Timber>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Timber>(timbers.AsQueryable().Provider));
        mockDbSetTimbers.As<IQueryable<Timber>>().Setup(m => m.Expression).Returns(timbers.AsQueryable().Expression);
        mockDbSetTimbers.As<IQueryable<Timber>>().Setup(m => m.ElementType).Returns(timbers.AsQueryable().ElementType);
        mockDbSetTimbers.As<IQueryable<Timber>>().Setup(m => m.GetEnumerator()).Returns(() => timbers.GetEnumerator());
        
        var mockDbSetCrops = new Mock<DbSet<Crops>>();
        mockDbSetCrops.As<IQueryable<Crops>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Crops>(new List<Crops> { crops }.AsQueryable().Provider));
        mockDbSetCrops.As<IQueryable<Crops>>().Setup(m => m.Expression).Returns(new List<Crops> { crops }.AsQueryable().Expression);
        mockDbSetCrops.As<IQueryable<Crops>>().Setup(m => m.ElementType).Returns(new List<Crops> { crops }.AsQueryable().ElementType);
        mockDbSetCrops.As<IQueryable<Crops>>().Setup(m => m.GetEnumerator()).Returns(() => new List<Crops> { crops }.GetEnumerator());

        var mockDbSetHerbs = new Mock<DbSet<Herbs>>();
        mockDbSetHerbs.As<IQueryable<Herbs>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Herbs>(new List<Herbs> { herbs }.AsQueryable().Provider));
        mockDbSetHerbs.As<IQueryable<Herbs>>().Setup(m => m.Expression).Returns(new List<Herbs> { herbs }.AsQueryable().Expression);
        mockDbSetHerbs.As<IQueryable<Herbs>>().Setup(m => m.ElementType).Returns(new List<Herbs> { herbs }.AsQueryable().ElementType);
        mockDbSetHerbs.As<IQueryable<Herbs>>().Setup(m => m.GetEnumerator()).Returns(() => new List<Herbs> { herbs }.GetEnumerator());
        
        var mockDbSetWeaponry = new Mock<DbSet<Weaponry>>();
        mockDbSetWeaponry.As<IQueryable<Weaponry>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Weaponry>(new List<Weaponry> { weaponry }.AsQueryable().Provider));
        mockDbSetWeaponry.As<IQueryable<Weaponry>>().Setup(m => m.Expression).Returns(new List<Weaponry> { weaponry }.AsQueryable().Expression);
        mockDbSetWeaponry.As<IQueryable<Weaponry>>().Setup(m => m.ElementType).Returns(new List<Weaponry> { weaponry }.AsQueryable().ElementType);
        mockDbSetWeaponry.As<IQueryable<Weaponry>>().Setup(m => m.GetEnumerator()).Returns(() => new List<Weaponry> { weaponry }.GetEnumerator());
        
        var mockDbSetMedicine = new Mock<DbSet<Medicine>>();
        mockDbSetMedicine.As<IQueryable<Medicine>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Medicine>(new List<Medicine> { medicine }.AsQueryable().Provider));
        mockDbSetMedicine.As<IQueryable<Medicine>>().Setup(m => m.Expression).Returns(new List<Medicine> { medicine }.AsQueryable().Expression);
        mockDbSetMedicine.As<IQueryable<Medicine>>().Setup(m => m.ElementType).Returns(new List<Medicine> { medicine }.AsQueryable().ElementType);
        mockDbSetMedicine.As<IQueryable<Medicine>>().Setup(m => m.GetEnumerator()).Returns(() => new List<Medicine> { medicine }.GetEnumerator());
        
        var mockDbSetWood = new Mock<DbSet<Wood>>();
        mockDbSetWood.As<IQueryable<Wood>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Wood>(new List<Wood> { wood }.AsQueryable().Provider));
        mockDbSetWood.As<IQueryable<Wood>>().Setup(m => m.Expression).Returns(new List<Wood> { wood }.AsQueryable().Expression);
        mockDbSetWood.As<IQueryable<Wood>>().Setup(m => m.ElementType).Returns(new List<Wood> { wood }.AsQueryable().ElementType);
        mockDbSetWood.As<IQueryable<Wood>>().Setup(m => m.GetEnumerator()).Returns(() => new List<Wood> { wood }.GetEnumerator());

        var mockDbContext = new Mock<ColonySimulatorContext>(CreateNewContextOptions());
        mockDbContext.Setup(c => c.Farmers).Returns(mockDbSetFarmers.Object);
        mockDbContext.Setup(c => c.Crops).Returns(mockDbSetCrops.Object);
        mockDbContext.Setup(c => c.Herbs).Returns(mockDbSetHerbs.Object);
        mockDbContext.Setup(c => c.Medics).Returns(mockDbSetMedics.Object);
        mockDbContext.Setup(c => c.BlackSmiths).Returns(mockDbSetBlacksmiths.Object);
        mockDbContext.Setup(c => c.Apothecaries).Returns(mockDbSetApothecaries.Object);
        mockDbContext.Setup(c => c.Traders).Returns(mockDbSetTraders.Object);
        mockDbContext.Setup(c => c.Timbers).Returns(mockDbSetTimbers.Object);
        mockDbContext.Setup(c => c.Medicines).Returns(mockDbSetMedicine.Object);
        mockDbContext.Setup(c => c.Medicines).Returns(mockDbSetMedicine.Object);
        mockDbContext.Setup(c => c.Wood).Returns(mockDbSetWood.Object);
        mockDbContext.Setup(c => c.Weaponry).Returns(mockDbSetWeaponry.Object);
        
        var mockThreatProvider = new Mock<IThreatProvider>();
        mockThreatProvider.Setup(tp => tp.ThreatToExperience).Returns(new FightingThreat {Id = 1, Name = "Army", ThreatLevel = 2, RequiredSmithingLevel = 3, RequiredWeaponryCount = 2});
        
        var handler = new ProfessionHandler(mockFarmerHandler.Object, mockApothecaryHandler.Object, mockBlackSmithHandler.Object, mockMedicHandler.Object, mockTimberHandler.Object, mockTraderHandler.Object, mockDbContext.Object, mockThreatHandler.Object, mockThreatProvider.Object);

        // Act
        await handler.HandleFarm();
        await handler.HandleMedic();
        await handler.HandleBlackSmith();
        await handler.HandleApothecary();
        await handler.HandleTrader();
        await handler.HandleTimber();

        // Assert
        mockFarmerHandler.Verify(fh => fh.Farm(It.IsAny<Crops>(), It.IsAny<Herbs>(), It.IsAny<int>()), Times.Exactly(farmers.Count));
        mockFarmerHandler.Verify(fh => fh.ExperienceThreat(It.IsAny<Effect>(), It.IsAny<Farmer>(), It.IsAny<List<Resource>>()), Times.Exactly(farmers.Count));
        
        mockMedicHandler.Verify(mh => mh.Heal(It.IsAny<Medicine>(), It.IsAny<Person>(), It.IsAny<int>()), Times.Exactly(medics.Count));
        mockMedicHandler.Verify(mh => mh.ExperienceThreat(It.IsAny<Effect>(), It.IsAny<Medic>(), It.IsAny<List<Resource>>()), Times.Exactly(medics.Count));

        mockBlackSmithHandler.Verify(bsh => bsh.CreateWeapon(It.IsAny<Weaponry>(), It.IsAny<Wood>(), It.IsAny<int>()), Times.Exactly(blackSmiths.Count));
        mockBlackSmithHandler.Verify(bsh => bsh.ExperienceThreat(It.IsAny<Effect>(), It.IsAny<BlackSmith>(), It.IsAny<List<Resource>>()), Times.Exactly(blackSmiths.Count));

        mockApothecaryHandler.Verify(ah => ah.CollectingHerbs(It.IsAny<Herbs>(), It.IsAny<int>()), Times.Exactly(0));
        mockApothecaryHandler.Verify(ah => ah.CreateMedicine(It.IsAny<Herbs>(), It.IsAny<Medicine>(), It.IsAny<int>()), Times.Exactly(0));
        mockApothecaryHandler.Verify(ah => ah.ExperienceThreat(It.IsAny<Effect>(), It.IsAny<Apothecary>(), It.IsAny<List<Resource>>()), Times.Exactly(0));

        mockTraderHandler.Verify(th => th.Trade(It.IsAny<Crops>(), It.IsAny<Wood>(), It.IsAny<Medicine>(), It.IsAny<Herbs>(), It.IsAny<Weaponry>()), Times.Exactly(0));
        mockTraderHandler.Verify(th => th.ExperienceThreat(It.IsAny<Effect>(), It.IsAny<Trader>(), It.IsAny<List<Resource>>()), Times.Exactly(0));
    }
}