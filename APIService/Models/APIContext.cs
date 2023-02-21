using BookWorldStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIService.Models
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Order> orders { get; set; }

        public DbSet<OrderDetail> ordersDetail { get; set; }

        public DbSet<Book> books { get; set; }
    }
}
