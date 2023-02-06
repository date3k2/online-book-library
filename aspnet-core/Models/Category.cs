using System.Text.Json.Serialization;

namespace BookProject.Models;

/// <summary>
/// Các loại sách
/// </summary>
public partial class Category
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<BookInfo> BookInfos { get; } = new List<BookInfo>();
}
