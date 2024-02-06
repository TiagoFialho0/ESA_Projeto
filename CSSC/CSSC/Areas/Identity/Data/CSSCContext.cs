using CSSC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSSC.Areas.Identity.Data;

public class CSSCContext : IdentityDbContext<CSSCUser>
{
    public CSSCContext(DbContextOptions<CSSCContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserEntityConfiguration());
    }
}

public class UserEntityConfiguration : IEntityTypeConfiguration<CSSCUser>
{
    public void Configure(EntityTypeBuilder<CSSCUser> builder)
    {
       // builder.Property(x => x.Email).HasMaxLength(250);
      //  builder.Property(x => x.Password).HasMaxLength(250);
        builder.Property(x => x.UtDataDeNascimento).HasMaxLength(10);
        builder.Property(x => x.UtNIF);
        builder.Property(x => x.UtMorada).HasMaxLength(250);
       // builder.Property(x => x.UtContactoTelefonico).HasMaxLength(250);
    }
}
