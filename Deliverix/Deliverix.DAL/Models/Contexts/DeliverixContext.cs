using Deliverix.Common.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Deliverix.DAL.Models.Contexts;

public class DeliverixContext : DbContext
{
    public DeliverixContext()
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(AppConfiguration.GetConfiguration("Entities", "ConnectionStrings"));
        }
    }
}