using GymManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Data.Configurations
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

            builder.Property(p => p.FullName)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(50);

            builder.Property(p => p.Phone)
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();

            builder.Property(p => p.Age)
            .HasColumnType("int");

            builder.Property(p => p.Email)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(p => p.Password)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(p => p.Gender)
            .HasConversion<string>()
            .IsRequired();

            builder.HasDiscriminator(p=>p.Role)
            .HasValue<Member>("Member")
            .HasValue<Coach>("Coach")
            .HasValue<Admin>("Admin");
            
            builder.ToTable("StaffAndMembers");

        }
    }
}