namespace CoEvent.Core.Extensions;

/// <summary>
/// DateTimeExtensions static class, provides extension methods for DateTime objects.
/// </summary>
public static class DateTimeExtensions
{
  /// <summary>
  /// Convert the specified timestamp into a DateTime object.
  /// </summary>
  /// <param name="timestamp"></param>
  /// <returns></returns>
  public static DateTime ConvertFromUnixTimestamp(this double timestamp)
  {
    return DateTime.UnixEpoch.AddSeconds(timestamp);
  }

  /// <summary>
  /// Converts the specified date into a Unix Epoch time value.
  /// </summary>
  /// <param name="date"></param>
  /// <returns></returns>
  public static double ConvertToUnixTimestamp(this DateTime date)
  {
    return Math.Floor((date.ToUniversalTime() - DateTime.UnixEpoch).TotalSeconds);
  }
}
