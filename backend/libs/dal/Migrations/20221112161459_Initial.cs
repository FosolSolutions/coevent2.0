using System;
using CoEvent.DAL;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoEvent.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : SqlServerSeedMigration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            PreUp(migrationBuilder);
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "''"),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "''"),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    FailedLogins = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LastLoginOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Version = table.Column<byte[]>(type: "varbinary(max)", nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Version = table.Column<byte[]>(type: "varbinary(max)", nullable: true, defaultValueSql: "0"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "''"),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Claim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Version = table.Column<byte[]>(type: "varbinary(max)", nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claim_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Version = table.Column<byte[]>(type: "varbinary(max)", nullable: true, defaultValueSql: "0"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "''"),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Version = table.Column<byte[]>(type: "varbinary(max)", nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => new { x.UserId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_UserAccount_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAccount_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => new { x.RoleId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_RoleClaim_Claim_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountType_OwnerId_Name",
                table: "Account",
                columns: new[] { "AccountType", "OwnerId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Key",
                table: "Account",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_Name",
                table: "Account",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_OwnerId",
                table: "Account",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Claim_AccountId",
                table: "Claim",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Claim_ClaimType_Value",
                table: "Claim",
                columns: new[] { "ClaimType", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_AccountId",
                table: "Role",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Key",
                table: "Role",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_ClaimId",
                table: "RoleClaim",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_User_Key",
                table: "User",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_AccountId",
                table: "UserAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
            PostUp(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            PreDown(migrationBuilder);
            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Claim");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "User");
            PostDown(migrationBuilder);
        }
    }
}
