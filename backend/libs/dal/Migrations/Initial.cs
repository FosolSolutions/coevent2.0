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
  #region Variables
  private readonly HashPassword _hashPassword = new();
  private long _scheduleId = 1;
  private long _eventId = 1;
  private long _activityId = 1;
  private long _openingId = 1;
  #endregion

  #region Methods
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
        nameof(User.EmailVerifiedOn),
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

  private void AddSchedule(MigrationBuilder migrationBuilder)
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
        _scheduleId,
        $"Victoria Ecclesia {startOn.Year} - {startOn:MMM} to {endOn:MMM}",
        1,
        startOn,
        endOn,
        true,
        "seed",
        "seed",
      });

    AddMemorialEvents(migrationBuilder, startOn, endOn);
    AddLectureEvents(migrationBuilder, startOn, endOn);
    AddBibleClassEvents(migrationBuilder, startOn, endOn);
    AddHallCleaningEvents(migrationBuilder, startOn, endOn);
    _scheduleId++;
  }

  private void AddMemorialEvents(MigrationBuilder migrationBuilder, DateTime startOn, DateTime endOn)
  {
    var date = new DateTime(startOn.Year, startOn.Month, startOn.GetFirstDayOfWeekInMonth(DayOfWeek.Sunday), 11, 0, 0);
    while (date <= endOn)
    {
      AddEvent(migrationBuilder, date, date.AddHours(1.5), "Memorial");

      AddActivity(migrationBuilder, date, "Presiding");
      AddOpening(migrationBuilder, "Preside", 1, "", false, "preside");
      _activityId++;
      _openingId++;

      AddActivity(migrationBuilder, date, "Exhorting", "Encouragement");
      AddOpening(migrationBuilder, "Exhort", 1, "", false, "exhort");
      _activityId++;
      _openingId++;

      AddActivity(migrationBuilder, date, "Pianist");
      AddOpening(migrationBuilder, "Pianist", 1, "", false, "piano");
      _activityId++;
      _openingId++;

      AddActivity(migrationBuilder, date, "Doorman");
      AddOpening(migrationBuilder, "Doorman", 1, "", false, "doorman");
      _activityId++;
      _openingId++;

      AddActivity(migrationBuilder, date, "Readings");
      AddOpening(migrationBuilder, "Read", 2, "", false, "read");
      _activityId++;
      _openingId++;

      AddActivity(migrationBuilder, date, "Prayers");
      AddOpening(migrationBuilder, "Bread", 1, "", false, "pray");
      _openingId++;
      AddOpening(migrationBuilder, "Wine", 1, "", false, "pray");
      _openingId++;
      AddOpening(migrationBuilder, "Close", 1, "", false, "pray");
      _openingId++;
      _activityId++;

      date = date.AddDays(7);
      _eventId++;
    }
  }

  private void AddLectureEvents(MigrationBuilder migrationBuilder, DateTime startOn, DateTime endOn)
  {
    var date = new DateTime(startOn.Year, startOn.Month, startOn.GetFirstDayOfWeekInMonth(DayOfWeek.Sunday), 19, 0, 0);
    while (date <= endOn)
    {
      int? seriesId = date switch
      {
        { Month: 2, Day: < 19 } => 3, // Q&A
        _ => null,
      };
      AddEvent(migrationBuilder, date, date.AddHours(1), "Bible Talk", seriesId);
      AddActivity(migrationBuilder, date, "Speaking", "Lecture");
      AddOpening(migrationBuilder, "Speak", 1, "Title", true, "lecture");
      _activityId++;
      _openingId++;

      date = date.AddDays(7);
      _eventId++;
    }
  }

  private void AddBibleClassEvents(MigrationBuilder migrationBuilder, DateTime startOn, DateTime endOn)
  {
    var date = new DateTime(startOn.Year, startOn.Month, startOn.GetFirstDayOfWeekInMonth(DayOfWeek.Thursday), 19, 30, 0);
    while (date <= endOn)
    {
      int? seriesId = date switch
      {
        { Month: 3 } => 1, // Character Study
        { Month: 4 } => 2, // Theme Study
        { Month: 5, Day: < 14 } => 4, // Group Discussion
        _ => null,
      };
      AddEvent(migrationBuilder, date, date.AddHours(1), "Bible Class", seriesId);
      AddActivity(migrationBuilder, date, "Speaking", "Lecture");
      AddOpening(migrationBuilder, "Speak", 1, "Title", true, "speak");
      _activityId++;
      _openingId++;

      date = date.AddDays(7);
      _eventId++;
    }
  }

  private void AddHallCleaningEvents(MigrationBuilder migrationBuilder, DateTime startOn, DateTime endOn)
  {
    var date = new DateTime(startOn.Year, startOn.Month, startOn.GetFirstDayOfWeekInMonth(DayOfWeek.Saturday), 08, 00, 0);
    while (date <= endOn)
    {
      AddEvent(migrationBuilder, date, date.AddHours(2), "Hall Cleaning");
      AddActivity(migrationBuilder, date, "Cleaning");
      AddOpening(migrationBuilder, "Clean", 6, "", false);
      _activityId++;
      _openingId++;

      date = date.AddDays(7);
      _eventId++;
    }
  }

  private void AddEvent(MigrationBuilder migrationBuilder, DateTime startOn, DateTime endOn, string name, int? seriesId = null)
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
        nameof(ScheduleEvent.SeriesId),
        nameof(ScheduleEvent.CreatedBy),
        nameof(ScheduleEvent.UpdatedBy),
      },
      new object?[] {
        _eventId,
        name,
        _scheduleId,
        startOn,
        endOn,
        true,
        seriesId,
        "seed",
        "seed",
      });
  }

  private void AddActivity(MigrationBuilder migrationBuilder, DateTime date, string name, string format = "")
  {
    migrationBuilder.InsertData(
      nameof(EventActivity),
      new string[] {
        nameof(EventActivity.Id),
        nameof(EventActivity.Name),
        nameof(EventActivity.EventId),
        nameof(EventActivity.Format),
        nameof(EventActivity.StartOn),
        nameof(EventActivity.EndOn),
        nameof(EventActivity.IsEnabled),
        nameof(EventActivity.CreatedBy),
        nameof(EventActivity.UpdatedBy),
      },
      new object?[] {
        _activityId,
        name,
        _eventId,
        format,
        date,
        date.AddHours(1.5),
        true,
        "seed",
        "seed",
      });
  }

  private void AddOpening(MigrationBuilder migrationBuilder, string name, int limit, string question, bool required, string? attribute = null)
  {
    migrationBuilder.InsertData(
      nameof(ActivityOpening),
      new string[] {
        nameof(ActivityOpening.Id),
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
        _openingId,
        name,
        _activityId,
        limit,
        question,
        required,
        true,
        "seed",
        "seed",
      });

    if (!String.IsNullOrWhiteSpace(attribute))
    {
      migrationBuilder.InsertData(
        nameof(OpeningRequirement),
        new string[] {
          nameof(OpeningRequirement.OpeningId),
          nameof(OpeningRequirement.Name),
          nameof(OpeningRequirement.Value),
          nameof(OpeningRequirement.CreatedBy),
          nameof(OpeningRequirement.UpdatedBy),
        },
        new object?[] {
          _openingId,
          "attribute",
          attribute,
          "seed",
          "seed",
        });
    }
  }
  #endregion
}


