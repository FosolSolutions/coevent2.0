namespace CoEvent.API.Authentication;

using CoEvent.API.Models.Auth;

/// <summary>
/// get/set -
/// </summary>
public interface IAuthenticator
{
  /// <summary>
  /// get/set -
  /// </summary>
  string HashPassword(string password);

  /// <summary>
  /// get/set -
  /// </summary>
  Entities.User? FindUser(string username);

  /// <summary>
  /// get/set -
  /// </summary>
  Entities.User? FindUser(Guid key);

  /// <summary>
  /// get/set -
  /// </summary>
  Entities.User Validate(string username, string password);

  /// <summary>
  /// get/set -
  /// </summary>
  Task<TokenModel> AuthenticateAsync(Entities.User user);
}
