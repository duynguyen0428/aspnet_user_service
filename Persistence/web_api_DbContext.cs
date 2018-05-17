using System;
using Microsoft.EntityFrameworkCore;
using web_api.Model;
namespace web_api.Persistence
{
    public class web_api_DbContext : DbContext {
        public web_api_DbContext(DbContextOptions<web_api_DbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users {get;set;}
    }
}