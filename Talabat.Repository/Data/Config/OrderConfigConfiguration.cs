using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Config
{
    internal class OrderConfigConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, S => S.WithOwner()); // 1:1 [Total Participation]

            builder.Property(o => o.Status)
                .HasConversion(
                    
                    OStatus => OStatus.ToString(),
                    OStatus => (OrderStatus) Enum.Parse(typeof(OrderStatus), OStatus)
                );

            builder.Property(o => o.Subtotal)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.DelivryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
