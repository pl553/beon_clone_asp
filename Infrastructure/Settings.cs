namespace Beon.Settings
{
  public static class DisplayName 
  {
    public const int MinLength = 1;
    public const int MaxLength = 64;
    public const string Regex = "^.*";
  }

  public static class UserName
  {
    public const int MinLength = 1;
    public const int MaxLength = 32;
    //allow alphanumeric with single hyphens inbetween
    public const string Regex = "^[A-Za-z0-9]+(?:-[A-Za-z0-9]+)*";
  }

  public static class Password
  {
    public const int MinLength = 6;
    public const int MaxLength = 128;
  }
}