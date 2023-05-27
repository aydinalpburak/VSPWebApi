using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;


namespace NewProject.Database
{
    public static class ValueConversionExtensions
    {
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class, new()
        {
            ValueConverter<T, string> converter = new ValueConverter<T, string>
            (
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<T>(v) ?? new T()
            );

            ValueComparer<T> comparer = new ValueComparer<T>
            (
                (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
                v => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(v))
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            propertyBuilder.HasColumnType("jsonb");

            return propertyBuilder;
        }
    }
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }

        public DbSet<VSPWebApi.API.Database.Models.eczane_tracker.Root> mytable { get; set; } = null!;
        public DbSet<VSPWebApi.API.Database.Models.eczane_tracker.Favori> favoriler { get; set; } = null!;
        public DbSet<VSPWebApi.API.Database.Models.eczane_tracker.EczaneKonumlari> eczanekonumlari { get; set; } = null!;
        public DbSet<VSPWebApi.API.Database.Models.eczane_tracker.Receteler> receteler { get; set; } = null!;
        public DbSet<VSPWebApi.API.Database.Models.eczane_tracker.Eczaneler> eczanevestokbilgisi { get; set; } = null!;
        public DbSet<VSPWebApi.API.Database.Models.eczane_tracker.Orders> orders { get; set; } = null!;
        public DbSet<VSPWebApi.API.Database.Models.eczane_tracker.UserLogin> users { get; set; } = null!;
        public DbSet<VSPWebApi.API.Database.Models.eczane_tracker.EczaneLogin> pharmacists { get; set; } = null!;




        public DataContext()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();



            var config = builder.Build();
            optionsBuilder.UseNpgsql(config.GetConnectionString("Database"));

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {




        }
    }
}