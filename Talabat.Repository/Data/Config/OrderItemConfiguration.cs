namespace Flavorful.Repository.Data.Config
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(oi => oi.Product, p => p.WithOwner());

            builder.Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
