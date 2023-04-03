using HotelListing.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configs
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.CountryCode).IsRequired();

            builder.Property(p => p.Name).HasColumnType("nvarchar(100)");
            builder.Property(p => p.CountryCode).HasColumnType("varchar(10)");



            builder.HasData(
                new Country
                {
                    Id = 1,
                    Name = "Egypt",
                    CountryCode = "EG"
                },
                new Country
                {
                    Id = 2,
                    Name = "United States",
                    CountryCode = "USA"
                },
                new Country
                {
                    Id = 3,
                    Name = "Bahamas",
                    CountryCode = "BS"
                }
                );
        }
    }
}
