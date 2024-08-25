namespace Talabat.Repository.Data.Config
{
    internal class ProductCategoryConfigrations : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired();
        }
    }
}
