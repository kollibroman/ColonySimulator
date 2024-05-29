using ColonySimulator.Backend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Moq;

namespace ColonySimulator.tests;

public class DbContextTest
{
     private DbContextOptions<ColonySimulatorContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<ColonySimulatorContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void CanInitializeDbSets()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Act
        using (var context = new ColonySimulatorContext(options))
        {
            // Assert
            Assert.NotNull(context.Apothecaries);
            Assert.NotNull(context.BlackSmiths);
            Assert.NotNull(context.Farmers);
            Assert.NotNull(context.Medics);
            Assert.NotNull(context.Timbers);
            Assert.NotNull(context.Traders);
            Assert.NotNull(context.Crops);
            Assert.NotNull(context.Medicines);
            Assert.NotNull(context.Weaponry);
            Assert.NotNull(context.Wood);
            Assert.NotNull(context.Herbs);
            Assert.NotNull(context.FightingThreats);
            Assert.NotNull(context.NaturalThreats);
            Assert.NotNull(context.PlagueThreats);
            Assert.NotNull(context.Proffesions);
        }
    }

    [Fact]
    public void OnModelCreating_ConfiguresModelBuilder()
    {
        // Arrange
        var options = CreateNewContextOptions();
        var conventionSet = new ConventionSet();
        var modelBuilder = new ModelBuilder(conventionSet);

        // Act
        var context = new TestableColonySimulatorContext(options);
        context.TestOnModelCreating(modelBuilder);

        // Assert
        var model = modelBuilder.Model;
        Assert.NotNull(model);
    }

    private class TestableColonySimulatorContext : ColonySimulatorContext
    {
        public TestableColonySimulatorContext(DbContextOptions<ColonySimulatorContext> options)
            : base(options)
        {
        }

        public void TestOnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreating(modelBuilder);
        }
    }
}