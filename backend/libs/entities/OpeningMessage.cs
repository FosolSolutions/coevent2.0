namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class OpeningMessage : AuditColumns
{
  #region Properties
  /// <summary>
  /// get/set - 
  /// </summary>
  public long Id { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public long OpeningId { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public ActivityOpening? Opening { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public long OwnerId { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public User? Owner { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public string Message { get; set; } = "";
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected OpeningMessage() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="opening"></param>
  /// <param name="owner"></param>
  /// <param name="message"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public OpeningMessage(ActivityOpening opening, User owner, string message)
  {
    this.Opening = opening ?? throw new ArgumentNullException(nameof(opening));
    this.OpeningId = opening.Id;
    this.Owner = owner ?? throw new ArgumentNullException(nameof(owner));
    this.OwnerId = owner.Id;
    this.Message = message;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="openingId"></param>
  /// <param name="ownerId"></param>
  /// <param name="message"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public OpeningMessage(long openingId, long ownerId, string message)
  {
    this.OpeningId = openingId;
    this.OwnerId = ownerId;
    this.Message = message;
  }
  #endregion
}
