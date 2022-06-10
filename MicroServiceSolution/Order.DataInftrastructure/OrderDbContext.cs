using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.OrderAggregate;
using Aggragtes = Order.Domain.OrderAggregate.Order;

namespace Order.DataInftrastructure
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.ApplyConfiguration(new OrderConfiguration());
            //???
            modelBuilder.Entity<Aggragtes>().OwnsOne(o => o.Address);

        }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Aggragtes> Orders { get; set; }

        


    }


    //public class OrderConfiguration : IEntityTypeConfiguration<Aggragtes>
    //{
    //    public void Configure(EntityTypeBuilder<Aggragtes> builder)
    //    {
    //        builder.HasKey(x => x.Id);
    //        builder.OwnsOne(x => x.Address);
    //    }
    //}
}