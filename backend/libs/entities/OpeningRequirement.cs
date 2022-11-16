namespace CoEvent.Entities;

/// <summary>
/// OpeningRequirement class, provides a model to store opening requirements in the database.
/// A requirement is a way to limit which users can apply.
/// </summary>
public class OpeningRequirement : AuditColumns
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the opening.
  /// </summary>
  public long OpeningId { get; set; }

  /// <summary>
  /// get/set - The opening.
  /// </summary>
  public ActivityOpening? Opening { get; set; }

  /// <summary>
  /// get/set - The name value;
  /// </summary>
  public string Name { get; set; } = "";

  /// <summary>
  /// get/set - The requirement value.
  /// </summary>
  public string Value { get; set; } = "";
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  public OpeningRequirement() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="opening"></param>
  /// <param name="name"></param>
  /// <param name="value"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public OpeningRequirement(ActivityOpening opening, string name, string value)
  {
    this.Opening = opening ?? throw new ArgumentNullException(nameof(opening));
    this.OpeningId = opening.Id;
    this.Name = name;
    this.Value = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="openingId"></param>
  /// <param name="name"></param>
  /// <param name="value"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public OpeningRequirement(long openingId, string name, string value)
  {
    this.OpeningId = openingId;
    this.Name = name;
    this.Value = value;
  }
  #endregion

  #region Methods
  #endregion
}
