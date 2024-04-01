using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSSC.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UtDataDeNascimento = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UtNIF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtMorada = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notificacao",
                columns: table => new
                {
                    IdNotif = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdServico = table.Column<int>(type: "int", nullable: false),
                    DataInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntervaloDeEnvio = table.Column<int>(type: "int", nullable: true),
                    TipoDeNotif = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacao", x => x.IdNotif);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceModel",
                columns: table => new
                {
                    IdServico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServIdUtilizador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    csscUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServIdOperador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    csscOperadorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServMarcaVeiculo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServModeloVeiculo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServMatriculaVeiculo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServClassificacao = table.Column<int>(type: "int", nullable: true),
                    ServComentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServPrazo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoDoServico = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceModel", x => x.IdServico);
                    table.ForeignKey(
                        name: "FK_ServiceModel_AspNetUsers_csscOperadorId",
                        column: x => x.csscOperadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceModel_AspNetUsers_csscUserId",
                        column: x => x.csscUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServicesStates",
                columns: table => new
                {
                    IdEstadoServico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServIdServico = table.Column<int>(type: "int", nullable: false),
                    servicesIdServico = table.Column<int>(type: "int", nullable: true),
                    EstadoDoServico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alteracaoEstado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesStates", x => x.IdEstadoServico);
                    table.ForeignKey(
                        name: "FK_ServicesStates_ServiceModel_servicesIdServico",
                        column: x => x.servicesIdServico,
                        principalTable: "ServiceModel",
                        principalColumn: "IdServico");
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UtDataDeNascimento", "UtMorada", "UtNIF" },
                values: new object[] { "3e303350-d578-4a3a-abbb-1f9b76454f8e", 0, "4e495d17-889d-47c1-9196-64c89a71d9af", "tiagofialho2002@gmail.com", false, false, null, null, null, null, null, false, "5294de24-b49d-48ed-8f77-aeba3acf0828", false, "Tiago", "01/01/1970", "Rua das Laranjas", "123456789" });

            migrationBuilder.InsertData(
                table: "ServiceModel",
                columns: new[] { "IdServico", "EstadoDoServico", "ServClassificacao", "ServComentario", "ServIdOperador", "ServIdUtilizador", "ServMarcaVeiculo", "ServMatriculaVeiculo", "ServModeloVeiculo", "ServPrazo", "csscOperadorId", "csscUserId" },
                values: new object[,]
                {
                    { 500, "Em espera", null, null, new Guid("97d0a3ff-e183-452d-8af1-5789c4fd7207"), new Guid("3e303350-d578-4a3a-abbb-1f9b76454f8e"), "Fiat", "AA-00-BB", "Punto", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Local), null, null },
                    { 510, "Em reparação", null, null, new Guid("97d0a3ff-e183-452d-8af1-5789c4fd7207"), new Guid("3e303350-d578-4a3a-abbb-1f9b76454f8e"), "Seat", "BB-11-CC", "Ibiza", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Local), null, null },
                    { 520, "Pronto para entrega", null, null, new Guid("97d0a3ff-e183-452d-8af1-5789c4fd7207"), new Guid("3e303350-d578-4a3a-abbb-1f9b76454f8e"), "Ford", "CC-22-DD", "Fiesta", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Local), null, null },
                    { 530, "Reparação concluida", null, null, new Guid("97d0a3ff-e183-452d-8af1-5789c4fd7207"), new Guid("3e303350-d578-4a3a-abbb-1f9b76454f8e"), "Ferrari", "DD-33-EE", "F40", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Local), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_csscOperadorId",
                table: "ServiceModel",
                column: "csscOperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_csscUserId",
                table: "ServiceModel",
                column: "csscUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesStates_servicesIdServico",
                table: "ServicesStates",
                column: "servicesIdServico");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Notificacao");

            migrationBuilder.DropTable(
                name: "ServicesStates");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ServiceModel");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
