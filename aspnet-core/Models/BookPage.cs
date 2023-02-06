using System.Text.Json.Serialization;

namespace BookProject.Models;

/// <summary>
/// Các trang sách
/// </summary>
public partial class BookPage
{
    public Guid BookId { get; set; }

    public short PageNumber { get; set; }

    public byte[] Content { get; set; } = null!;

    public bool? IsMarked { get; set; }

    [JsonIgnore]
    public virtual BookInfo Book { get; set; } = null!;
}
