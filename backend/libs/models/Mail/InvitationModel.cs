namespace CoEvent.Models.Mail;

/// <summary>
/// 
/// </summary>
public class InvitationModel
{
  #region Properties
  /// <summary>
  /// get/set - URL to the web application.
  /// </summary>
  public Uri Url { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public ScheduleModel Schedule { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public UserModel From { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public UserModel To { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="url"></param>
  /// <param name="schedule"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  public InvitationModel(Uri url, ScheduleModel schedule, UserModel from, UserModel to)
  {
    this.Url = url;
    this.Schedule = schedule;
    this.From = from;
    this.To = to;
  }
  #endregion
}
