using GymManagementSystem.Models;
using GymManagementSystem.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementSystem.Data.Configurations
{
    public class MemberConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(p => p.Weight)
            .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Height)
            .HasColumnType("decimal(18,2)");

            builder.Property(p => p.SessionsAvailable)
            .HasColumnType("int");

            builder.Property(p => p.IsPrivate)
            .IsRequired()
            .HasColumnType("Bit");

            builder.HasOne(p => p.Coach)
            .WithMany(p => p.Members)
            .HasForeignKey(p => p.CoachId)
            .IsRequired(false);

            builder.HasOne(p => p.Subscription)
            .WithOne(p => p.Member)
            .HasForeignKey<Subscription>(s => s.MemberId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        }
    }
}