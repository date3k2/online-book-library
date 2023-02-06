using System.Text.Json.Serialization;

namespace BookProject.Models;

public partial class BookInfo
{
    /// <summary>
    /// ID sách
    /// </summary>
    public Guid BookId { get; set; }

    /// <summary>
    /// Tên/ Tựa đề sách
    /// </summary>
    public string Title { get; set; } = null!;

    public Guid CategoryId { get; set; }

    /// <summary>
    /// Tác giả
    /// </summary>
    public string Author { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    /// <summary>
    /// Thời gian tạo
    /// </summary>
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Thời gian chỉnh sửa
    /// </summary>
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    /// Số trang
    /// </summary>
    public int NumberOfPages { get; set; }
    [JsonIgnore]
    public virtual ICollection<BookPage> BookPages { get; } = new List<BookPage>();
    [JsonIgnore]
    public virtual Category Category { get; set; } = null!;
}
