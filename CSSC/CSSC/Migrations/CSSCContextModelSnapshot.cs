using System;
using CSSC.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CSSC.Migrations
{
    [DbContext(typeof(CSSCContext))]
    partial class CSSCContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CSSC.Areas.Identity.Data.CSSCUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UtDataDeNascimento")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UtMorada")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("UtNIF")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "3e303350-d578-4a3a-abbb-1f9b76454f8e",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9939112f-c7bb-40ea-b62d-1b4353a6d630",
                            Email = "tiagofialho2002@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e5eb5685-ca5a-4111-baf2-fa7342cb3f21",
                            TwoFactorEnabled = false,
                            UserName = "Tiago",
                            UtDataDeNascimento = "01/01/1970",
                            UtMorada = "Rua das Laranjas",
                            UtNIF = 123456789
                        });
                });

            modelBuilder.Entity("CSSC.Models.Notificacao", b =>
                {
                    b.Property<int>("IdNotif")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNotif"), 1L, 1);

                    b.Property<DateTime>("DataInicial")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdServico")
                        .HasColumnType("int");

                    b.Property<int?>("IntervaloDeEnvio")
                        .HasColumnType("int");

                    b.Property<string>("TipoDeNotif")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdNotif");

                    b.ToTable("Notificacao");
                });

            modelBuilder.Entity("CSSC.Models.Services", b =>
                {
                    b.Property<int>("IdServico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdServico"), 1L, 1);

                    b.Property<string>("EstadoDoServico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ServClassificacao")
                        .HasColumnType("int");

                    b.Property<string>("ServComentario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ServIdOperador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServIdUtilizador")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ServMarcaVeiculo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServMatriculaVeiculo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServModeloVeiculo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ServPrazo")
                        .HasColumnType("datetime2");

                    b.Property<string>("csscOperadorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("csscUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IdServico");

                    b.HasIndex("csscOperadorId");

                    b.HasIndex("csscUserId");

                    b.ToTable("ServiceModel");

                    b.HasData(
                        new
                        {
                            IdServico = 500,
                            EstadoDoServico = "Em espera",
                            ServIdOperador = new Guid("97d0a3ff-e183-452d-8af1-5789c4fd7207"),
                            ServIdUtilizador = new Guid("3e303350-d578-4a3a-abbb-1f9b76454f8e"),
                            ServMarcaVeiculo = "Fiat",
                            ServMatriculaVeiculo = "AA-00-BB",
                            ServModeloVeiculo = "Punto",
                            ServPrazo = new DateTime(2024, 5, 26, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            IdServico = 510,
                            EstadoDoServico = "Em reparação",
                            ServIdOperador = new Guid("97d0a3ff-e183-452d-8af1-5789c4fd7207"),
                            ServIdUtilizador = new Guid("3e303350-d578-4a3a-abbb-1f9b76454f8e"),
                            ServMarcaVeiculo = "Seat",
                            ServMatriculaVeiculo = "BB-11-CC",
                            ServModeloVeiculo = "Ibiza",
                            ServPrazo = new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            IdServico = 520,
                            EstadoDoServico = "Pronto para entrega",
                            ServIdOperador = new Guid("97d0a3ff-e183-452d-8af1-5789c4fd7207"),
                            ServIdUtilizador = new Guid("3e303350-d578-4a3a-abbb-1f9b76454f8e"),
                            ServMarcaVeiculo = "Ford",
                            ServMatriculaVeiculo = "CC-22-DD",
                            ServModeloVeiculo = "Fiesta",
                            ServPrazo = new DateTime(2024, 7, 26, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            IdServico = 530,
                            EstadoDoServico = "Reparação Concluida",
                            ServIdOperador = new Guid("97d0a3ff-e183-452d-8af1-5789c4fd7207"),
                            ServIdUtilizador = new Guid("3e303350-d578-4a3a-abbb-1f9b76454f8e"),
                            ServMarcaVeiculo = "Ferrari",
                            ServMatriculaVeiculo = "DD-33-EE",
                            ServModeloVeiculo = "F40",
                            ServPrazo = new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Local)
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CSSC.Models.Services", b =>
                {
                    b.HasOne("CSSC.Areas.Identity.Data.CSSCUser", "csscOperador")
                        .WithMany()
                        .HasForeignKey("csscOperadorId");

                    b.HasOne("CSSC.Areas.Identity.Data.CSSCUser", "csscUser")
                        .WithMany()
                        .HasForeignKey("csscUserId");

                    b.Navigation("csscOperador");

                    b.Navigation("csscUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CSSC.Areas.Identity.Data.CSSCUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CSSC.Areas.Identity.Data.CSSCUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CSSC.Areas.Identity.Data.CSSCUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CSSC.Areas.Identity.Data.CSSCUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
