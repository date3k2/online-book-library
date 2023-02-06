using BookProject.Models;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksContext _context;

        public BooksController(BooksContext dbContext)
        {
            _context = dbContext;
        }
        /// <summary>
        /// Find book info by Id
        /// </summary>
        /// <param name="bookId">Book Id</param>
        /// <returns> Book Info</returns>
        [HttpGet("BookInfoById")]
        public async Task<ActionResult> GetBookInfo(Guid bookId)
        {
            var bookInfo = await _context.BookInfos.FindAsync(bookId);
            return StatusCode(200, new { bookInfo!.BookId, bookInfo.Category.CategoryId, bookInfo.NumberOfPages, bookInfo.Category.CategoryName, bookInfo.Title, bookInfo!.Author });
        }

        [HttpGet("BookMarks")]
        public async Task<ActionResult> GetBookMarks([FromQuery] Guid bookId)
        {
            var markedPages = await _context.BookPages.Where(x => x.BookId == bookId && x.IsMarked == true).Select(x =>
                x.PageNumber).ToListAsync();
            return StatusCode(200, markedPages);
        }

        [HttpGet]
        public async Task<ActionResult> GetBooksInfo(string? keyword, Guid? categoryId, int pageNumber)
        {
            var booksInfo = await _context.BookInfos.ToListAsync();

            booksInfo = booksInfo.Where(x => (categoryId == null || x.CategoryId == categoryId)
            && (keyword == null || (x.Title.ToLower().Contains(keyword.ToLower()) || x.Author.ToLower().Contains(keyword.ToLower())))).ToList();

            return StatusCode(200, booksInfo.Skip(10 * pageNumber).Take(10).Select(x => new { x.BookId, x.NumberOfPages, x.Category.CategoryId, x.Category.CategoryName, x.Title, x.Author }));
        }

        // GET: api/Books
        [HttpGet("Categories")]
        public async Task<ActionResult> GetCategories()
        {
            return StatusCode(200, await _context.Categories.ToListAsync());
        }

        [HttpGet("GetPage")]
        public async Task<ActionResult> GetPage([FromQuery] Guid bookId, [FromQuery] short pageNumber)
        {
            var page = await _context.BookPages.FindAsync(bookId, pageNumber);
            if (page == null)
            {
                return NotFound();
            }
            return File(page.Content, "application/pdf");
        }

        [HttpGet("TotalBooksByCategory")]
        public async Task<ActionResult> GetTotalNumberOfBooksByCatOrKeyWord(string? keyword, Guid? categoryId)
        {
            return StatusCode(200, await _context.BookInfos.CountAsync(x => (categoryId == null || x.CategoryId == categoryId)
                        && (keyword == null || (x.Title.ToLower().Contains(keyword.ToLower()) || x.Author.ToLower().Contains(keyword.ToLower())))));
        }

        [HttpPost("1")]
        public async Task<ActionResult> Post([FromForm] Book book)
        {
            if (book.CategoryName != null)
            {
                var category = new Category
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = book.CategoryName
                };
                await _context.Categories.AddAsync(category);
                book.CategoryId = category.CategoryId.ToString();
            }

            using var pdfReader = new PdfReader(book.File.OpenReadStream());
            using var pdfDocument = new PdfDocument(pdfReader);
            int numPages = pdfDocument.GetNumberOfPages();
            var bookInfo = new BookInfo
            {
                Title = book.Title,
                BookId = Guid.NewGuid(),
                Author = book.Author,
                NumberOfPages = numPages,
                CategoryId = new Guid(book.CategoryId),
                ContentType = book.File.ContentType,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            await _context.BookInfos.AddAsync(bookInfo);
            for (short pageNum = 1; pageNum <= numPages; pageNum++)
            {
                PdfPage page = pdfDocument.GetPage(pageNum);
                using (var newPdfDocument = new PdfDocument(new PdfWriter("output.pdf")))
                {
                    PdfPage newPage = newPdfDocument.AddNewPage();

                    // Get the size of the original PdfPage
                    var pageSize = page.GetMediaBox();

                    // Set the size of the new PdfPage to the size of the original PdfPage
                    newPage.SetMediaBox(pageSize);

                    // Get the PdfCanvas object for the new PdfPage
                    var canvas = new PdfCanvas(newPage);

                    // Create a PdfFormXObject from the PdfPage
                    PdfFormXObject form = page.CopyAsFormXObject(newPdfDocument);
                    canvas.AddXObject(form);

                    // Close the PdfDocument to finish writing the PDF file
                    newPdfDocument.Close();
                }
                var pages = new List<BookPage>();
                using (var fileStream = new FileStream("output.pdf", FileMode.Open))
                {
                    // Read the bytes from the file
                    byte[] pdfBytes = new byte[fileStream.Length];
                    fileStream.Read(pdfBytes, 0, (int)fileStream.Length);
                    var newPage = new BookPage
                    {
                        BookId = bookInfo.BookId,
                        PageNumber = pageNum,
                        IsMarked = false,
                        Content = pdfBytes
                    };
                    pages.Add(newPage);
                }
                _context.BookPages.AddRange(pages);
            }
            FileInfo k = new("output.pdf");
            k.Delete();
            // save the changes to the database
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPost]
        public async Task<ActionResult> PostBook([FromForm] Book book)
        {
            if (book.CategoryName != null)
            {
                var category = new Category
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = book.CategoryName
                };
                await _context.Categories.AddAsync(category);
                book.CategoryId = category.CategoryId.ToString();
            }

            using var pdfReader = new PdfReader(book.File.OpenReadStream());
            using var pdfDocument = new PdfDocument(pdfReader);
            int numPages = pdfDocument.GetNumberOfPages();
            var bookInfo = new BookInfo
            {
                Title = book.Title,
                BookId = Guid.NewGuid(),
                Author = book.Author,
                NumberOfPages = numPages,
                CategoryId = new Guid(book.CategoryId),
                ContentType = book.File.ContentType,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            await _context.BookInfos.AddAsync(bookInfo);
            var pages = new List<BookPage>();

            for (short pageNum = 1; pageNum <= numPages; pageNum++)
            {
                var memoryStream = new MemoryStream(1000000);
                PdfPage page = pdfDocument.GetPage(pageNum);
                using (var newPdf = new PdfDocument(new PdfWriter(memoryStream)))
                {
                    PdfPage newPag = newPdf.AddNewPage().SetMediaBox(page.GetMediaBox());
                    // Get the PdfCanvas object for the new PdfPage
                    var canvas = new PdfCanvas(newPag);

                    // Create a PdfFormXObject from the PdfPage
                    PdfFormXObject form = page.CopyAsFormXObject(newPdf);
                    canvas.AddXObject(form);
                    newPdf.Close();
                }
                byte[] pdfBytes = memoryStream.ToArray();
                var newPage = new BookPage
                {
                    BookId = bookInfo.BookId,
                    PageNumber = pageNum,
                    IsMarked = false,
                    Content = pdfBytes
                };
                pages.Add(newPage);
            }
            _context.BookPages.AddRange(pages);
            await _context.SaveChangesAsync();
            return StatusCode(201);

            // save the changes to the database
        }

        [HttpPut("ToggleBookMark")]
        public async Task<ActionResult> ToggleBookMark([FromQuery] Guid bookId, [FromQuery] short pageNumber)
        {
            var page = await _context.BookPages.FirstAsync(x => x.BookId == bookId && x.PageNumber == pageNumber);
            if (page == null)
            {
                return NotFound();
            }
            page.IsMarked = !page.IsMarked;
            await _context.SaveChangesAsync();
            return StatusCode(200, page.IsMarked);
        }
    }
}