﻿using CSSC.Areas.Identity.Data;
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
                Id = "3e303350-d578-4a3a-abbb-1f9b76454f8e",
                Email = "tiagofialho2002@gmail.com",
                UserName = "Tiago",
                UtDataDeNascimento = "01/01/1970",
                UtMorada = "Rua das Laranjas",
                UtNIF = "123456789",
            }
            );

        builder.Entity<Services>().HasData(
                    new Services
                    {
                        IdServico = 500,
                        ServIdUtilizador = Guid.Parse("3e303350-d578-4a3a-abbb-1f9b76454f8e"),
                        ServIdOperador = Guid.Parse("35117df6-e153-43d0-83b9-ab7c1d8a2249"),
                        ServMarcaVeiculo = "Fiat",
                        ServModeloVeiculo = "Punto",
                        ServMatriculaVeiculo = "AA-00-BB",
                        ServDataInicio = System.DateTime.Today.AddMonths(2),
                        EstadoDoServico = "Em espera"
                    },
                    new Services
                    {
                        IdServico = 510,
                        ServIdUtilizador = Guid.Parse("3e303350-d578-4a3a-abbb-1f9b76454f8e"),
                        ServIdOperador = Guid.Parse("35117df6-e153-43d0-83b9-ab7c1d8a2249"),
                        ServMarcaVeiculo = "Seat",
                        ServModeloVeiculo = "Ibiza",
                        ServMatriculaVeiculo = "BB-11-CC",
                        ServDataInicio = System.DateTime.Today.AddMonths(1),
                        EstadoDoServico = "Em reparação"
                    },
                    new Services
                    {
                        IdServico = 520,
                        ServIdUtilizador = Guid.Parse("3e303350-d578-4a3a-abbb-1f9b76454f8e"),
                        ServIdOperador = Guid.Parse("35117df6-e153-43d0-83b9-ab7c1d8a2249"),
                        ServMarcaVeiculo = "Ford",
                        ServModeloVeiculo = "Fiesta",
                        ServMatriculaVeiculo = "CC-22-DD",
                        ServDataInicio = System.DateTime.Today.AddMonths(4),
                        EstadoDoServico = "Pronto para entrega"
                    },
                    new Services
                    {
                        IdServico = 530,
                        ServIdUtilizador = Guid.Parse("3e303350-d578-4a3a-abbb-1f9b76454f8e"),
                        ServIdOperador = Guid.Parse("35117df6-e153-43d0-83b9-ab7c1d8a2249"),
                        ServMarcaVeiculo = "Ferrari",
                        ServModeloVeiculo = "F40",
                        ServMatriculaVeiculo = "DD-33-EE",
                        ServDataInicio = System.DateTime.Today.AddMonths(1),
                        EstadoDoServico = "Reparação concluida"
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
    public DbSet<CSSC.Models.ServicesStates>? ServicesStates { get; set; }
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
