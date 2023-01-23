using APBD9.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APBD9.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.IdUser).HasName("User_PK");
            builder.Property(x => x.IdUser).UseIdentityColumn();

            builder.Property(x => x.Login).HasMaxLength(35).IsRequired();
            builder.HasIndex(x => x.Login).IsUnique();

            builder.Property(x => x.Password).HasMaxLength(70).IsRequired();

            builder.Property(x => x.Salt).IsRequired();
            builder.Property(x => x.RefreshToken).IsRequired();
            builder.Property(x => x.RefreshTokenExp).IsRequired();
        }
    }
}
