using Microsoft.EntityFrameworkCore;
using Net5_Core.Models;

namespace Net5_Infrastructure.Data
{
    public class Net5DataContext : DbContext
    {
        public Net5DataContext(DbContextOptions<Net5DataContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
