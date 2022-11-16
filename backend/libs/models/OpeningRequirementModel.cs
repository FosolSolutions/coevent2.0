namespace CoEvent.Models;

/// <summary>
/// 
/// </summary>
public class OpeningRequirementModel : AuditColumnsModel
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public long OpeningId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public ActivityOpeningModel? Opening { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string Name { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public string Value { get; set; } = "";
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an OpeningRequirementModel object.
  /// </summary>
  public OpeningRequirementModel() { }

  /// <summary>
  /// Creates a new instance of an OpeningRequirementModel object, initializes with specified parameters.
  /// </summary>
  /// <param name="requirement"></param>
  public OpeningRequirementModel(Entities.OpeningRequirement requirement) : base(requirement)
  {
    this.OpeningId = requirement.OpeningId;
    this.Opening = requirement.Opening != null ? new ActivityOpeningModel(requirement.Opening) : null;
    this.Name = requirement.Name;
    this.Value = requirement.Value;
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="model"></param>
  public static explicit operator Entities.OpeningRequirement(OpeningRequirementModel model)
  {
    return new Entities.OpeningRequirement(model.OpeningId, model.Name, model.Value)
    {
      Version = model.Version,
    };
  }
  #endregion
}
