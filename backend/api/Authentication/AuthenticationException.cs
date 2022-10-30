namespace CoEvent.API.Authentication;

using System;

/// <summary>
/// AuthenticationException class, provides an exception for authentication errors.
/// </summary>
public class AuthenticationException : Exception
{
  #region Constructors
  /// <summary>
  /// Creates a new instance of an AuthenticationException object.
  /// </summary>
  public AuthenticationException() : base()
  {

  }

  /// <summary>
  /// Creates a new instance of an AuthenticationException object, initializes with the specified parameters.
  /// </summary>
  /// <param name="message"></param>
  public AuthenticationException(string message) : base(message)
  {

  }
  #endregion
}
