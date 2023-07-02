namespace TestKSK.Interfaces
{
    public interface IDbContextInitializer
    {
        Task Migrate(CancellationToken cancellationToken = default);
        Task Seed(CancellationToken cancellationToken = default);
    }
}
