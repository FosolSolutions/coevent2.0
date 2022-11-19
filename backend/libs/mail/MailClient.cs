using System.Net.Mail;
using CoEvent.Core.Extensions;
using CoEvent.Models;
using CoEvent.Models.Mail;
using Microsoft.Extensions.Options;

namespace CoEvent.Mail;

/// <summary>
/// MailClient class, provides a way to send emails.
/// </summary>
public class MailClient
{
  #region Variables
  private readonly MailOptions _options;
  private readonly SmtpClient _client;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of a MailClient object, and initializes it with the specified options.
  /// </summary>
  /// <param name="options"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public MailClient(MailOptions options)
  {
    _options = options ?? throw new ArgumentNullException(nameof(options));

    _client = new SmtpClient(_options.Host)
    {
      Port = _options.Port,
      UseDefaultCredentials = _options.UseDefaultCredentials,
      Credentials = new System.Net.NetworkCredential(!String.IsNullOrWhiteSpace(_options.Username) ? _options.Username : _options.FromEmail, _options.Password),
      EnableSsl = _options.EnableSsl,
      Timeout = _options.Timeout
    };
  }

  /// <summary>
  /// Creates a new instance of a MailClient object, and initializes it with the specified options.
  /// </summary>
  /// <param name="options"></param>
  public MailClient(IOptions<MailOptions> options) : this(options.Value)
  {
  }
  #endregion

  #region Methods
  /// <summary>
  /// Create a mail message to invite the user to the calendar.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  public MailMessage CreateInvitation(InvitationModel model)
  {
    // TODO: Move this to some kind of templating service.
    // TODO: Need to include the current domain name instead of hardcoding it.
    var mail = new MailMessage
    {
      From = new MailAddress(_options.FromEmail),
      Subject = "Victoria Ecclesial Volunteer Schedule",
      IsBodyHtml = true,
      Body = $@"
				Hello {model.To.FirstName},
        <div>
          <p>
            The updated online volunteer schedule now has a few new features.
          </p>
          <ul>
            <li>Works on mobile devices</li>
            <li>Highlights recommended series</li>
            <li>Provides a way for everyone to request a topic or submit questions</li>
          </ul>
        </div>
        <p>
					Please use the following link to access the Victoria Ecclesial Volunteer Schedule - <a href=""{model.Url}/login?key={model.To.Key}&redirect_uri=/schedules/{model.Schedule.Id}"">VOLUNTEER LINK</a><br/>
					This link is specifically generated for you, please do not forward it to someone else.
				</p>
        <p>
          Love in Christ,<br/>
          {model.From.FirstName}<br/>
          {(!String.IsNullOrWhiteSpace(model.From.Phone) ? $"m: {model.From.Phone}" : "")}
        </p>"
    };
    mail.To.Add(new MailAddress(model.To.Email));
    return mail;
  }

  /// <summary>
  /// Send email to applicants on behalf of user.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  public MailMessage CreateOpeningMessage(OpeningMessageMailModel model)
  {
    var mail = new MailMessage
    {
      From = new MailAddress(_options.FromEmail),
      Subject = "Victoria Ecclesial Volunteer Schedule - Message",
      IsBodyHtml = true,
      Body = $@"
				Hello {model.To.FirstName},
        <p>
					The following message has been submitted by {model.Message.Owner?.DisplayName} to you regarding the opening to '{model.Message.Opening?.Name}' for the date {model.Message.Opening?.Activity?.StartOn:MMM dd, yyyy}.
				</p>
        <hr/>
        <p>
          {model.Message.Message}
        </p>
        <hr/>
        <p>
          Please use the following link to access the Victoria Ecclesial Volunteer Schedule - <a href=""{model.Url}/login?key={model.To.Key}&redirect_uri=/schedules/{model.Message.Opening?.Activity?.Event?.ScheduleId}"">VOLUNTEER LINK</a>
        </p>
        <p>
          Love in Christ,<br/>
          {model.From.FirstName}<br/>
          {(!String.IsNullOrWhiteSpace(model.From.Phone) ? $"m: {model.From.Phone}" : "")}
        </p>"
    };
    mail.To.Add(new MailAddress(model.To.Email));
    return mail;
  }

  /// <summary>
  /// Send the specified email message.
  /// </summary>
  /// <param name="message"></param>
  public void Send(MailMessage message)
  {
    _client.Send(message);
  }

  /// <summary>
  /// Send the specified email message asynchronously.
  /// </summary>
  /// <param name="message"></param>
  /// <returns></returns>
  public async Task SendAsync(MailMessage message)
  {
    await _client.SendMailAsync(message);
  }
  #endregion
}
