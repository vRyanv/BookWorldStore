using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIService.Models
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }
        public DbSet<UserViewModel> users { get; set; }
    }
}
