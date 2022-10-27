namespace Beon.Infrastructure
{
  public static class IEnumerableExtensions
  {
    public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> enumerable) where T : class
    {
      return enumerable.Where(e => e != null).Select(e => e!);
    }
  }
}