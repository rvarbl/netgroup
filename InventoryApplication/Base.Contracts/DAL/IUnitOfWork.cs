namespace Base.Contracts.DAL;

public interface IUnitOfWork
{
    public int SaveChanges();
    public Task<int> SaveChangesAsync();
}