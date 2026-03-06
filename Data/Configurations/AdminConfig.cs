using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Data.Configurations
{
    public class AdminConfig : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(p => p.Shifts)
            .HasConversion<string>()
            .IsRequired();

            builder.HasData( new Admin
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    FullName = "Sayed Ahmed",
                    Email = "admin@gym.com",
                    Password = "$2a$11$WXHKRbxbTYUP8.EDYMqs8ej/E1/3oEurvQMl2O2tCbXTd/.PYmn4G", //Admin123@ 
                    Phone = "01099999999",
                    Age = 32,
                    Gender = Gender.Male,
                    Shifts = Shifts.Morning
                },
                new Admin
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    FullName = "Nour El-Sayed",
                    Email = "support@gym.com",
                    Password = "$2a$11$BGh.QBPmTb2/ag0t10C2Z.e86xsq5hBZQVOcXHb6vzAoXUMNtlea6",  // Support123@
                    Phone = "01188888888",
                    Age = 26,
                    Gender = Gender.Female,
                    Shifts = Shifts.Evening
                }
);
        }
    }
}