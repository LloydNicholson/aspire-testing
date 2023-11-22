namespace Aspire.WebApi;

public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<NewEntityTable>();
    }
}

public class NewEntityTable
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
