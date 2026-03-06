using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Data.Configurations
{
    public class CoachConfig : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.Property(p=>p.ExperienceYears)
            .HasColumnType("int");

            builder.HasData(new Coach
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                FullName = "Ahmed Ali",
                Email = "ahmed.ali@gym.com",
                Password = "$2a$11$MSGydp8Ry8O2TB.ucwg3I.XpUvqjt1J8fjPkXJrOmxeW7uItZHKxe", // 11111111Aa@
                Phone = "01000000000",
                Age = 35,
                Gender = Gender.Male,
                ExperienceYears = 6
            },
            new Coach
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                FullName = "Sara Mohamed",
                Email = "sara.mohamed@gym.com",
                Password = "$2a$11$MA/THxwDyYVgaDrqqhUAzuhoeNlVyaiEE0mUuWW71Y1ZLuRR7Ltvq", // 222222222Sm@
                Phone = "01111111111",
                Age = 30,
                Gender = Gender.Female,
                ExperienceYears =3
            },
            new Coach
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                FullName = "Omar Hassan",
                Email = "omar.hassan@gym.com",
                Password = "$2a$11$O5iOsLS9ZaSE9nwSC8tNz.V1qaQRn3hzbyuKFwW/ywgTBr5M8h5Sq", // 33333333Oh@
                Phone = "01222222222",
                Age = 40,
                Gender = Gender.Male,
                ExperienceYears = 7

            }
            );
        }
    }
}