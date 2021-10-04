using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MathWeb.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MathWeb.Models
{
    public class ConnectionStringClass : IdentityDbContext<IdentityUser>
    {
        public DbSet<TaskMath> Tasks { get; set; }
        public ConnectionStringClass(DbContextOptions<ConnectionStringClass> options) : base(options) { }
    }
}
