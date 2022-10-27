using Microsoft.EntityFrameworkCore;
using Beon.Models;

namespace Beon.Infrastructure
{
  public static class IQueryableExtensions
  {
    public static IQueryable<T> TakePage<T>(this IQueryable<T> query, int page) where T : Topic
      =>  page <= 0 ? throw new ArgumentException("Page must be > 1", nameof(page))
        : query.OrderByDescending(t => t.PostId)
          .Skip(Beon.Settings.Page.ItemCount * (page - 1))
          .Take(Beon.Settings.Page.ItemCount);

    public static async Task<int> CountNumberOfPagesAsync<T>(this IQueryable<T> query) where T : Topic
      => (await query.CountAsync() + Beon.Settings.Page.ItemCount - 1) / Beon.Settings.Page.ItemCount;
  }
}