using System.Net.Mail;
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
  /// <param name="invitation"></param>
  /// <returns></returns>
  public MailMessage CreateInvitation(InvitationModel invitation)
  {
    // TODO: Move this to some kind of templating service.
    // TODO: Need to include the current domain name instead of hardcoding it.
    var message = new MailMessage
    {
      From = new MailAddress(_options.FromEmail),
      Subject = "Victoria Ecclesial Volunteer Schedule",
      IsBodyHtml = true,
      Body = $@"
				Hello {invitation.To.FirstName},
                <p>
					Please use the following link to access the Victoria Ecclesial Volunteer Schedule - <a href=""{invitation.Url}/login?key={invitation.To.Key}&redirect_uri=/schedules/{invitation.Schedule.Id}"">VOLUNTEER LINK</a><br/>
					This link is specifically generated for you, please do not forward it to someone else.
				</p>
                <p>
				    Love in Christ, {invitation.From.FirstName}<br/>
                    m: {invitation.From.Phone}<br/>
                    e: {invitation.From.Email}
                </p>"
    };
    message.To.Add(new MailAddress(invitation.To.Email));
    return message;
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
