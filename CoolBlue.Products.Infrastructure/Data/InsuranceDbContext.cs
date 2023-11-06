using CoolBlue.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoolBlue.Products.Infrastructure.Data
{
	public class InsuranceDbContext : DbContext
	{
		public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options) { }
		public DbSet<ProductType> ProductTypes { get; set; }
	
	}
}
