using Microsoft.EntityFrameworkCore;
using OrdersPoc.Infrastructure.Data;

namespace OrdersPoc.IntegrationTests.Helpers;

public class TestDatabaseFixture : IDisposable
{
    public ApplicationDbContext Context { get; private set; }

    public TestDatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new ApplicationDbContext(options);
        Context.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}