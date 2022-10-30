namespace CoEvent.API.Models.Tokens;

/// <summary>
/// ParticipantLoginModel class, provides a model that authenticates a participant key.
/// </summary>
public class ParticipantLoginModel
{
  #region Properties
  /// <summary>
  /// The participant key to identify the user with.
  /// </summary>
  public Guid Key { get; set; }
  #endregion
}
