using ColonySimulator.tests.TestAsyncQuery;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ColonySimulator.tests.Helpers;

public class DbContextHelpers
{
    public static Mock<T> GetDbContextMock<T>() where T : DbContext
    {
        var optionsBuilder = new DbContextOptionsBuilder<T>();
        return new Mock<T>(optionsBuilder.Options);
    }
    
    public static DbSet<T> GetQueryableMockDbSet<T>(List<T> source)
        where T : class
    {
        var queryable = source.AsQueryable<T>();

        var dbSet = new Mock<DbSet<T>>();
        dbSet.As<IAsyncEnumerable<T>>().Setup(x => x.GetAsyncEnumerator(default)).Returns(new TestAsyncEnumerator<T>(queryable.GetEnumerator()));
        dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(queryable.Provider));
        dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
        dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => source.Add(s));

        return dbSet.Object;
    }
}