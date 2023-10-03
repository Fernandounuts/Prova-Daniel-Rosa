using Microsoft.EntityFrameworkCore;
using Prova_Rosa.Models;

namespace Prova_Rosa.Data;

public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			
		}

		public DbSet<User> Users { get; set; }
    }