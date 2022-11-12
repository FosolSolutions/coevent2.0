using CoEvent.Core.Encryption;
using CoEvent.Core.Extensions;
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
    AddSchedule(migrationBuilder);
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

  private static void AddSchedule(MigrationBuilder migrationBuilder)
  {
    var startOn = new DateTime(2023, 01, 01);
    var endOn = startOn.AddMonths(6).AddDays(DateTime.DaysInMonth(startOn.Year, 6) - 1);
    migrationBuilder.InsertData(
      nameof(Schedule),
      new string[] {
        nameof(Schedule.Id),
        nameof(Schedule.Name),
        nameof(Schedule.AccountId),
        nameof(Schedule.StartOn),
        nameof(Schedule.EndOn),
        nameof(Schedule.IsEnabled),
        nameof(Schedule.CreatedBy),
        nameof(Schedule.UpdatedBy),
      },
      new object?[] {
        1,
        $"Victoria Ecclesia {startOn.Year} - {startOn:MMM} to {endOn:MMM}",
        1,
        startOn,
        endOn,
        true,
        "seed",
        "seed",
      });

    var result = AddMemorialEvents(migrationBuilder, (1, 1, 1), startOn, endOn);
    result = AddLectureEvents(migrationBuilder, result, startOn, endOn);
    result = AddBibleClassEvents(migrationBuilder, result, startOn, endOn);
    result = AddHallCleaningEvents(migrationBuilder, result, startOn, endOn);
  }

  private static (long ScheduleId, long ActivityId, long EventId) AddMemorialEvents(MigrationBuilder migrationBuilder, (long ScheduleId, long ActivityId, long EventId) ids, DateTime startOn, DateTime endOn)
  {
    var date = new DateTime(startOn.Year, startOn.Month, startOn.GetFirstDayOfWeekInMonth(DayOfWeek.Sunday), 11, 0, 0);
    while (date <= endOn)
    {
      migrationBuilder.InsertData(
        nameof(ScheduleEvent),
        new string[] {
          nameof(ScheduleEvent.Id),
          nameof(ScheduleEvent.Name),
          nameof(ScheduleEvent.ScheduleId),
          nameof(ScheduleEvent.StartOn),
          nameof(ScheduleEvent.EndOn),
          nameof(ScheduleEvent.IsEnabled),
          nameof(ScheduleEvent.CreatedBy),
          nameof(ScheduleEvent.UpdatedBy),
        },
        new object?[] {
          ids.EventId,
          "Memorial",
          ids.ScheduleId,
          date,
          date.AddHours(1.5),
          true,
          "seed",
          "seed",
        });

      AddActivity(migrationBuilder, ids.ActivityId, ids.EventId, date, "Presiding");
      AddOpening(migrationBuilder, ids.ActivityId++, "Preside", 1, "", false);

      AddActivity(migrationBuilder, ids.ActivityId, ids.EventId, date, "Exhorting");
      AddOpening(migrationBuilder, ids.ActivityId++, "Exhort", 1, "", false);

      AddActivity(migrationBuilder, ids.ActivityId, ids.EventId, date, "Pianist");
      AddOpening(migrationBuilder, ids.ActivityId++, "Pianist", 1, "", false);

      AddActivity(migrationBuilder, ids.ActivityId, ids.EventId, date, "Doorman");
      AddOpening(migrationBuilder, ids.ActivityId++, "Doorman", 1, "", false);

      AddActivity(migrationBuilder, ids.ActivityId, ids.EventId, date, "Reading");
      AddOpening(migrationBuilder, ids.ActivityId++, "Read", 2, "", false);

      AddActivity(migrationBuilder, ids.ActivityId, ids.EventId++, date, "Prayers");
      AddOpening(migrationBuilder, ids.ActivityId, "Bread", 1, "", false);
      AddOpening(migrationBuilder, ids.ActivityId, "Wine", 1, "", false);
      AddOpening(migrationBuilder, ids.ActivityId++, "Close", 1, "", false);

      date = date.AddDays(7);
    }

    return ids;
  }

  private static (long ScheduleId, long ActivityId, long EventId) AddLectureEvents(MigrationBuilder migrationBuilder, (long ScheduleId, long ActivityId, long EventId) ids, DateTime startOn, DateTime endOn)
  {
    var date = new DateTime(startOn.Year, startOn.Month, startOn.GetFirstDayOfWeekInMonth(DayOfWeek.Sunday), 19, 0, 0);
    while (date <= endOn)
    {
      migrationBuilder.InsertData(
        nameof(ScheduleEvent),
        new string[] {
          nameof(ScheduleEvent.Id),
          nameof(ScheduleEvent.Name),
          nameof(ScheduleEvent.ScheduleId),
          nameof(ScheduleEvent.StartOn),
          nameof(ScheduleEvent.EndOn),
          nameof(ScheduleEvent.IsEnabled),
          nameof(ScheduleEvent.CreatedBy),
          nameof(ScheduleEvent.UpdatedBy),
        },
        new object?[] {
          ids.EventId,
          "Bible Talk",
          ids.ScheduleId,
          date,
          date.AddHours(1),
          true,
          "seed",
          "seed",
        });

      AddActivity(migrationBuilder, ids.ActivityId, ids.EventId, date, "Presiding");
      AddOpening(migrationBuilder, ids.ActivityId++, "Preside", 1, "", false);

      AddActivity(migrationBuilder, ids.ActivityId, ids.EventId++, date, "Speaking");
      AddOpening(migrationBuilder, ids.ActivityId++, "Speak", 1, "Title", true);

      date = date.AddDays(7);
    }

    return ids;
  }

  private static (long ScheduleId, long ActivityId, long EventId) AddBibleClassEvents(MigrationBuilder migrationBuilder, (long ScheduleId, long ActivityId, long EventId) ids, DateTime startOn, DateTime endOn)
  {
    var date = new DateTime(startOn.Year, startOn.Month, startOn.GetFirstDayOfWeekInMonth(DayOfWeek.Thursday), 19, 30, 0);
    while (date <= endOn)
    {
      migrationBuilder.InsertData(
        nameof(ScheduleEvent),
        new string[] {
          nameof(ScheduleEvent.Id),
          nameof(ScheduleEvent.Name),
          nameof(ScheduleEvent.ScheduleId),
          nameof(ScheduleEvent.StartOn),
          nameof(ScheduleEvent.EndOn),
          nameof(ScheduleEvent.IsEnabled),
          nameof(ScheduleEvent.CreatedBy),
          nameof(ScheduleEvent.UpdatedBy),
        },
        new object?[] {
          ids.EventId,
          "Bible Class",
          ids.ScheduleId,
          date,
          date.AddHours(1),
          true,
          "seed",
          "seed",
        });

      AddActivity(migrationBuilder, ids.ActivityId, ids.EventId, date, "Presiding");
      AddOpening(migrationBuilder, ids.ActivityId++, "Preside", 1, "", false);

      AddActivity(migrationBuilder, ids.ActivityId, ids.EventId++, date, "Speaking");
      AddOpening(migrationBuilder, ids.ActivityId++, "Speak", 1, "Title", true);

      date = date.AddDays(7);
    }

    return ids;
  }

  private static (long ScheduleId, long ActivityId, long EventId) AddHallCleaningEvents(MigrationBuilder migrationBuilder, (long ScheduleId, long ActivityId, long EventId) ids, DateTime startOn, DateTime endOn)
  {
    var date = new DateTime(startOn.Year, startOn.Month, startOn.GetFirstDayOfWeekInMonth(DayOfWeek.Saturday), 08, 00, 0);
    while (date <= endOn)
    {
      migrationBuilder.InsertData(
        nameof(ScheduleEvent),
        new string[] {
          nameof(ScheduleEvent.Id),
          nameof(ScheduleEvent.Name),
          nameof(ScheduleEvent.ScheduleId),
          nameof(ScheduleEvent.StartOn),
          nameof(ScheduleEvent.EndOn),
          nameof(ScheduleEvent.IsEnabled),
          nameof(ScheduleEvent.CreatedBy),
          nameof(ScheduleEvent.UpdatedBy),
        },
        new object?[] {
          ids.EventId,
          "Hall Cleaning",
          ids.ScheduleId,
          date,
          date.AddHours(2),
          true,
          "seed",
          "seed",
        });

      AddActivity(migrationBuilder, ids.ActivityId, ids.EventId++, date, "Cleaning");
      AddOpening(migrationBuilder, ids.ActivityId++, "Clean", 5, "", false);

      date = date.AddDays(7);
    }

    return ids;
  }

  private static void AddActivity(MigrationBuilder migrationBuilder, long activityId, long eventId, DateTime date, string name)
  {
    migrationBuilder.InsertData(
      nameof(EventActivity),
      new string[] {
        nameof(EventActivity.Id),
        nameof(EventActivity.Name),
        nameof(EventActivity.EventId),
        nameof(EventActivity.StartOn),
        nameof(EventActivity.EndOn),
        nameof(EventActivity.IsEnabled),
        nameof(EventActivity.CreatedBy),
        nameof(EventActivity.UpdatedBy),
      },
      new object?[] {
        activityId,
        name,
        eventId,
        date,
        date.AddHours(1.5),
        true,
        "seed",
        "seed",
      });
  }

  private static void AddOpening(MigrationBuilder migrationBuilder, long activityId, string name, int limit, string question, bool required)
  {
    migrationBuilder.InsertData(
      nameof(ActivityOpening),
      new string[] {
        nameof(ActivityOpening.Name),
        nameof(ActivityOpening.ActivityId),
        nameof(ActivityOpening.Limit),
        nameof(ActivityOpening.Question),
        nameof(ActivityOpening.ResponseRequired),
        nameof(ActivityOpening.IsEnabled),
        nameof(ActivityOpening.CreatedBy),
        nameof(ActivityOpening.UpdatedBy),
      },
      new object?[] {
        name,
        activityId,
        limit,
        question,
        required,
        true,
        "seed",
        "seed",
      });
  }
}


