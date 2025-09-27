using Domain.Entities;
using Domain.Entities.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BookConfigurations : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.BookId);

        builder.Property(x => x.Title)
            .HasMaxLength(BookConstraints.TitleMaxLength)
            .IsRequired();

        builder.OwnsOne(x => x.Isbn, isbn =>
        {
            isbn.Property(I => I.Value)
                .HasColumnName("Isbn")
                .IsRequired();

            isbn.HasIndex(x => x.Value)
                .IsUnique();
        });

        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(p => p.Amount)
                .HasPrecision(BookConstraints.PricePrecison, BookConstraints.PriceScale)
                .HasColumnName("Amount")
                .IsRequired();
            price.Property(p => p.Currency)
                .HasMaxLength(BookConstraints.ISOCodeMaxLength)
                .HasColumnName("PriceCurrency")
                .IsRequired();
        });
    }
}
