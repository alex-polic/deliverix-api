using Deliverix.DAL.Models.Contexts;

namespace Deliverix.DAL;

public class UnitOfWork
{
    public DeliverixContext Context { get; }

    public UnitOfWork()
    {
        Context = new DeliverixContext();
    }

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
}