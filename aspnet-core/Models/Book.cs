namespace BookProject.Models
{
    public class Book
    {
        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public IFormFile File { get; set; } = null!;

        public string CategoryId { get; set; } = null!;

        public string? CategoryName { get; set; }
    }
}
