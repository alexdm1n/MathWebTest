using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MathWeb.Models
{
    public class ConnectionStringClass : IdentityDbContext<IdentityUser>
    {
        public DbSet<TaskMath> Tasks { get; set; }
        public ConnectionStringClass(DbContextOptions<ConnectionStringClass> options) : base(options) { }

    }
}
