﻿namespace CoEvent.DAL;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;
using CoEvent.Entities;
using CoEvent.Core.Extensions;
using CoEvent.DAL.Extensions;
using CoEvent.DAL.Configuration;

/// <summary>
/// 
/// </summary>
public class CoEventContext : DbContext
{
  #region Variables
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly JsonSerializerOptions _serializerOptions;
  #endregion

  #region Properties
  // public virtual DbSet<Account> Accounts { get; set; } = null!;
  // public virtual DbSet<Application> Applications { get; set; } = null!;
  // public virtual DbSet<Calendar> Calendars { get; set; } = null!;
  // public virtual DbSet<Criteria> Criterias { get; set; } = null!;
  // public virtual DbSet<CriteriaTrait> CriteriaTraits { get; set; } = null!;
  // public virtual DbSet<Event> Events { get; set; } = null!;
  // public virtual DbSet<EventOccurrence> EventOccurrences { get; set; } = null!;
  // public virtual DbSet<Opening> Openings { get; set; } = null!;
  // public virtual DbSet<OpeningCriteria> OpeningCriterias { get; set; } = null!;
  // public virtual DbSet<OpeningOccurrence> OpeningOccurrences { get; set; } = null!;
  // public virtual DbSet<Participant> Participants { get; set; } = null!;
  // public virtual DbSet<Schedule> Schedules { get; set; } = null!;
  // public virtual DbSet<Survey> Surveys { get; set; } = null!;
  // public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; } = null!;
  // public virtual DbSet<Trait> Traits { get; set; } = null!;
  // public virtual DbSet<UserAccount> UserAccounts { get; set; } = null!;
  // public virtual DbSet<UserClaim> UserClaims { get; set; } = null!;

  /// <summary>
  /// 
  /// </summary>
  public virtual DbSet<User> Users { get; set; } = null!;

  /// <summary>
  /// 
  /// </summary>
  public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

  /// <summary>
  /// 
  /// </summary>
  public virtual DbSet<Role> Roles { get; set; } = default!;

  /// <summary>
  /// 
  /// </summary>
  public virtual DbSet<RoleClaim> RoleClaims { get; set; } = default!;

  /// <summary>
  /// 
  /// </summary>
  public virtual DbSet<Claim> Claims { get; set; } = default!;
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="options"></param>
  private CoEventContext(DbContextOptions<CoEventContext> options) : base(options)
  {
    _httpContextAccessor = new HttpContextAccessor();
    _serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="options"></param>
  /// <param name="contextAssessor"></param>
  /// <param name="serializerOptions"></param>
  public CoEventContext(DbContextOptions<CoEventContext> options, IHttpContextAccessor contextAssessor, IOptions<JsonSerializerOptions>? serializerOptions = null) : base(options)
  {
    _httpContextAccessor = contextAssessor;
    _serializerOptions = serializerOptions?.Value ?? new JsonSerializerOptions(JsonSerializerDefaults.Web);
  }
  #endregion

  #region Methods

  /// <summary>
  /// Configures the DbContext with the specified options.
  /// </summary>
  /// <param name="optionsBuilder"></param>
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      optionsBuilder.EnableSensitiveDataLogging();
    }

    base.OnConfiguring(optionsBuilder);
  }

  /// <summary>
  /// Apply all the configuration settings to the model.
  /// </summary>
  /// <param name="modelBuilder"></param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyAllConfigurations(typeof(UserConfiguration), this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int SaveChanges()
  {
    var username = _httpContextAccessor.HttpContext?.User.GetUsername() ?? "Unknown";
    var now = DateTime.UtcNow;

    foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State != EntityState.Detached && e.State != EntityState.Unchanged))
    {
      if (entry.Entity is AuditColumns entity)
      {
        switch (entry.State)
        {
          case EntityState.Added:
            entity.CreatedBy = username;
            entity.CreatedOn = now;
            goto case EntityState.Modified;
          case EntityState.Modified:
            entity.UpdatedBy = username;
            entity.UpdatedOn = now;
            break;
        }
      }
    }
    return base.SaveChanges();
  }

  /// <summary>
  /// Wrap the save changes in a transaction for rollback.
  /// </summary>
  /// <returns></returns>
  public int CommitTransaction()
  {
    var result = 0;
    using (var transaction = this.Database.BeginTransaction())
    {
      try
      {
        result = this.SaveChanges();
        transaction.Commit();
      }
      catch (DbUpdateException)
      {
        transaction.Rollback();
        throw;
      }
    }
    return result;
  }

  /// <summary>
  /// Deserialize the specified 'json' to the specified type of 'T'.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="json"></param>
  /// <returns></returns>
  public T? Deserialize<T>(string json)
  {
    return JsonSerializer.Deserialize<T>(json, _serializerOptions);
  }

  /// <summary>
  /// Serialize the specified 'item'.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="item"></param>
  /// <returns></returns>
  public string Serialize<T>(T item)
  {
    return JsonSerializer.Serialize(item, _serializerOptions);
  }
  #endregion
}
