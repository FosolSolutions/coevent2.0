namespace CoEvent.API.Authorization;

/// <summary>
/// RealmAccess private class, provides a way to deserialize the realm access.
/// </summary>
class RealmAccess
{
  public string[] Roles { get; set; } = Array.Empty<string>();
}
