namespace CoEvent.Models.Mail;

/// <summary>
/// 
/// </summary>
public class OpeningMessageMailModel
{
  #region Properties
  /// <summary>
  /// get/set - URL to the web application.
  /// </summary>
  public Uri Url { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public OpeningMessageModel Message { get; set; }

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
  /// <param name="message"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  public OpeningMessageMailModel(Uri url, OpeningMessageModel message, UserModel from, UserModel to)
  {
    this.Url = url;
    this.Message = message;
    this.From = from;
    this.To = to;
  }
  #endregion
}
