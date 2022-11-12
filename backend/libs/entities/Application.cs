namespace CoEvent.Entities;

/// <summary>
/// Application class, provides a model to manage user opening applications.
/// An application is a record of a user applying to an activity opening.
/// </summary>
public class Application : AuditColumns
{
  #region Properties
  /// <summary>
  /// get/set - Primary key.
  /// </summary>
  public long Id { get; set; }

  /// <summary>
  /// get/set - Foreign key to user who submitted the application.
  /// </summary>
  public long UserId { get; set; }

  /// <summary>
  /// get/set - The user.
  /// </summary>
  public User? User { get; set; }

  /// <summary>
  /// get/set - Foreign key to the activity opening.
  /// </summary>
  public long OpeningId { get; set; }

  /// <summary>
  /// get/set - The opening.
  /// </summary>
  public ActivityOpening? Opening { get; set; }

  /// <summary>
  /// get/set - A message to include in the application.
  /// </summary>
  public string Message { get; set; } = "";
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected Application() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="user"></param>
  /// <param name="opening"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public Application(User user, ActivityOpening opening)
  {
    this.User = user ?? throw new ArgumentNullException(nameof(user));
    this.UserId = user.Id;
    this.Opening = opening ?? throw new ArgumentNullException(nameof(opening));
    this.OpeningId = opening.Id;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="openingId"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public Application(long userId, long openingId)
  {
    this.UserId = userId;
    this.OpeningId = openingId;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="user"></param>
  /// <param name="opening"></param>
  /// <param name="message"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public Application(User user, ActivityOpening opening, string message) : this(user, opening)
  {
    this.Message = message;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="openingId"></param>
  /// <param name="message"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public Application(long userId, long openingId, string message) : this(userId, openingId)
  {
    this.Message = message;
  }
  #endregion
}
