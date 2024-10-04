namespace Flavorful.Repository.Data.Config
{
    internal class ProductBrandConfigrations : IEntityTypeConfiguration<ProductBrand>
    {
        public void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            builder.Property(b => b.Name)
                .IsRequired();
        }
    }
}
