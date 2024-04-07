using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<RaiffeisenRsd> RaiffeisenRsds { get; set; }
    }
}
