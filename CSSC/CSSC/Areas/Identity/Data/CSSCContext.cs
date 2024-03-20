using CSSC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CSSC.Models;
using System.Reflection.Emit;

namespace CSSC.Areas.Identity.Data;

public class CSSCContext : IdentityDbContext<CSSCUser>
{
    public CSSCContext(DbContextOptions<CSSCContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<CSSCUser>().HasData(
            new CSSCUser
            {
                Id = "asfknwdg-cdfn-dasd-lsdf-dasdasiclxcc",
                UtDataDeNascimento = "01/01/1970",
                UtMorada = "Rua das Laranjas",
                UtNIF = 123456789
            }
            );

        builder.Entity<Services>().HasData(
                    new Services
                    {
                        IdServico = 500,
                        ServIdUtilizador = Guid.Parse("asfknwdg-cdfn-dasd-lsdf-dasdasiclxcc"),
                        ServMarcaVeiculo = "Fiat",
                        ServModeloVeiculo = "Punto",
                        ServMatriculaVeiculo = "AA-00-BB",
                        ServPrazo = System.DateTime.Today.AddYears(5),
                        EstadoDoServico = "Em espera"
                    },
                    new Services
                    {
                        IdServico = 510,
                        ServIdUtilizador = Guid.Parse("asfknwdg-cdfn-dasd-lsdf-dasdasiclxcc"),
                        ServMarcaVeiculo = "Seat",
                        ServModeloVeiculo = "Ibiza",
                        ServMatriculaVeiculo = "BB-11-CC",
                        ServPrazo = System.DateTime.Today.AddYears(4),
                        EstadoDoServico = "Em reparação"
                    },
                    new Services
                    {
                        IdServico = 520,
                        ServIdUtilizador = Guid.Parse("asfknwdg-cdfn-dasd-lsdf-dasdasiclxcc"),
                        ServMarcaVeiculo = "Ford",
                        ServModeloVeiculo = "Fiesta",
                        ServMatriculaVeiculo = "CC-22-DD",
                        ServPrazo = System.DateTime.Today.AddYears(3),
                        EstadoDoServico = "Pronto para entrega"
                    },
                    new Services
                    {
                        IdServico = 530,
                        ServIdUtilizador = Guid.Parse("asfknwdg-cdfn-dasd-lsdf-dasdasiclxcc"),
                        ServMarcaVeiculo = "Ferrari",
                        ServModeloVeiculo = "F40",
                        ServMatriculaVeiculo = "DD-33-EE",
                        ServPrazo = System.DateTime.Today.AddYears(2),
                        EstadoDoServico = "Reparação Concluida"
                    }
                );

        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserEntityConfiguration());
    }

    public DbSet<CSSC.Models.Services>? ServiceModel { get; set; }
    public DbSet<CSSC.Models.Notificacao>? Notificacao { get;set; }
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
