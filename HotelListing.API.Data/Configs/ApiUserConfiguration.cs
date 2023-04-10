using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configs
{
    public class ApiUserConfiguration : IEntityTypeConfiguration<ApiUser>
    {
        public void Configure(EntityTypeBuilder<ApiUser> builder)
        {
            
            builder.Property(p => p.FirstName).HasColumnType("nvarchar(100)");
            builder.Property(p => p.LastName).HasColumnType("varchar(100)");
           
        }
    }
}
