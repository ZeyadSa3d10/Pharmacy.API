using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Models.PharmacyModels;

namespace Pharmacy.Infrastructure.Configration
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(P=> P.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(P => P.Description).IsRequired().HasMaxLength(1000);
            builder.Property(P => P.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(P => P.Barcode).IsRequired();
            builder.Property(P=>P.PrescriptionRequired).IsRequired();
            builder.Property(P=>P.ExpirationDate).IsRequired();
            builder.Property(P => P.StockQuantity).IsRequired();
            builder.Property(P => P.CategoryId).IsRequired();
            builder.Property(P => P.SupplierId).IsRequired();


            builder.HasOne(p => p.Category)
                   .WithMany()
                   .HasForeignKey(p => p.CategoryId);
            builder.HasOne(p => p.Supplier)
                   .WithMany()
                   .HasForeignKey(p => p.SupplierId);
        }
    }
}
