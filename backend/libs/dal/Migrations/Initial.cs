using CoEvent.Core.Encryption;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoEvent.DAL.Migrations;

/// <summary>
/// Initial class, provides a way to seed the database with code.
/// </summary>
public partial class Initial : SqlServerSeedMigration
{
  private readonly HashPassword _hashPassword = new();

  /// <summary>
  /// Insert the following data.
  /// </summary>
  /// <param name="migrationBuilder"></param>
  protected override void InsertData(MigrationBuilder migrationBuilder)
  {
    UpdateAdminPassword(migrationBuilder);

    // AddUsers(migrationBuilder);
    // AddAccounts(migrationBuilder);
    // AddUserAccounts(migrationBuilder);

  }

  private void UpdateAdminPassword(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.UpdateData("User", nameof(User.Id), 1, nameof(User.Password), _hashPassword.Hash(FactorySettings.DefaultPassword, FactorySettings.SaltLength));
  }

  private void AddUsers(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.InsertData(
      "User",
      new string[] {
        nameof(User.Id),
        nameof(User.Username),
        nameof(User.Email),
        nameof(User.Key),
        nameof(User.Password),
        nameof(User.DisplayName),
        nameof(User.FirstName),
        nameof(User.MiddleName),
        nameof(User.LastName),
        nameof(User.IsEnabled),
        nameof(User.FailedLogins),
        nameof(User.UserType),
        nameof(User.EmailVerified),
        nameof(User.VerifiedOn),
        nameof(User.CreatedBy),
        nameof(User.UpdatedBy)
      },
      new object?[] {
          1,
          "admin",
          "admin@fosol.ca",
          "24e8ae15-848f-44ee-8a79-e014f89c538e",
          _hashPassword.Hash(FactorySettings.DefaultPassword, FactorySettings.SaltLength),
          "Administrator",
          "System",
          "",
          "Administrator",
          true,
          0,
          (int)UserType.User,
          true,
          null,
          "seed",
          "seed"
        }
      );
  }

  private static void AddAccounts(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.InsertData(
      "Account",
      new string[] {
        nameof(Account.Id),
        nameof(Account.Name),
        nameof(Account.Description),
        nameof(Account.AccountType),
        nameof(Account.IsEnabled),
        nameof(Account.OwnerId),
        nameof(Account.CreatedBy),
        nameof(Account.UpdatedBy)
      },
      new object?[] {
        1,
        "CoEvent",
        "",
        (int)AccountType.Free,
        true,
        1,
        "seed",
        "seed"
      }
    );
  }

  private static void AddUserAccounts(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.InsertData("UserAccount",
      new string[] {
        nameof(UserAccount.UserId),
        nameof(UserAccount.AccountId)
      },
      new object?[] {
        1,
        1
      });
  }
}


