using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace HotelListing.API.Data.Configs
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Address).IsRequired();
            builder.Property(p => p.Rating).IsRequired();
            builder.Property(p => p.CountryId).IsRequired();

            builder.HasOne(p => p.Country)
                .WithMany(p => p.Hotels)
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Name).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Address).HasColumnType("nvarchar(200)");



            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Helnan",
                    Address = "Portsaid, Egypt",
                    Rating = 5,
                    CountryId = 1
                }
                );

        }
    }
}
