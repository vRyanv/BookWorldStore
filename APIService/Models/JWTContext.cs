using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIService.Models
{
    public class JWTContext : DbContext
    {
        public JWTContext(DbContextOptions<JWTContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
    }
}
