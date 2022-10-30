namespace CoEvent.Core.Extensions;

/// <summary>
/// 
/// </summary>
public static class EnumExtensions
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="TEnum"></typeparam>
  /// <param name="value"></param>
  /// <param name="defaultValue"></param>
  /// <returns></returns>
  public static TEnum TryParseEnum<TEnum>(this string value, TEnum defaultValue = default)
       where TEnum : struct
  {
    return Enum.TryParse<TEnum>(value, out TEnum result) ? result : defaultValue;
  }
}
