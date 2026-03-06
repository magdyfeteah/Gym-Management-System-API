using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using GymManagementSystem.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Data.Configurations
{
    public class SubscriptionConfig : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(p=>p.Id);

            builder.Property(p=>p.Plans)
            .HasConversion<string>();

            builder.Property(p=>p.Status)
            .HasConversion<string>()
            .IsRequired();

            builder.Property(p=>p.JoinDate)
            .HasColumnType("Date")
            .IsRequired();

            builder.Property(p=>p.EndDate)
            .HasColumnType("Date")
            .IsRequired();
            
            
            builder.ToTable("Subscriptions");
        }
    }
}