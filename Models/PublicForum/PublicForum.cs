/*
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beon.Models
{
  public class PublicForum
  {
    public int PublicForumId { get; set; }
    //required
    /// <summary>
    /// Descriptive name (e.g. Просто общение)
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// Name used in the path (e.g. talk -> domain.com/talk)
    /// </summary>
    public string PathName { get; set; } = null!;
    /// <summary>
    /// True if the forum is anonymous
    /// </summary>
    /// <remarks>
    /// see http://web.archive.org/web/20160205010440/http://beon.ru/anonymous/
    /// </remarks>
    public bool Anonymous { get; set; } = false;
    public ICollection<PublicForumTopic>? ForumTopics { get; set; }
  }
}
*/