using HotelListing.API.Data;
using HotelListing.API.Data.Configs;
using HotelListing.API.Repository;
using HotelListing.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace HotelListing.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("HotelListingDbConnectionString")
                )
            );

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(
                options => options.AddPolicy("AllowAll", 
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                )
            );

            builder.Host.UseSerilog((builder, loggerConfiguration) => 
                loggerConfiguration.WriteTo.Console()
                                .ReadFrom.Configuration(builder.Configuration));

            builder.Services.AddAutoMapper(typeof(MapperConfiguration));

            builder.Services.AddScoped<ICountryRepository, CountryRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}