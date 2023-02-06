using Microsoft.EntityFrameworkCore;
namespace BookProject.Models;

public partial class BooksContext : DbContext
{
    public BooksContext() { }
    public BooksContext(DbContextOptions<BooksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookInfo> BookInfos { get; set; }

    public virtual DbSet<BookPage> BookPages { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookInfo>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK_BookInfo_BookId");

            entity.ToTable("BookInfo");

            entity.Property(e => e.BookId)
                .HasComment("ID sách");
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .HasComment("Tác giả");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(36);
            entity.Property(e => e.ContentType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate)
                .HasComment("Thời gian tạo")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.ModifiedDate)
                .HasComment("Thời gian chỉnh sửa")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasComment("Tên/ Tựa đề sách");
            entity.Property(e => e.NumberOfPages).HasComment("Số trang");
            entity.HasOne(c => c.Category).WithMany(b => b.BookInfos)
            .HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BookPage>(entity =>
        {
            entity.HasKey("BookId", "PageNumber");
            entity.ToTable(tb => tb.HasComment("Các trang sách"));
            entity.HasOne(d => d.Book).WithMany(p => p.BookPages)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_BookPages_BookId");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable(tb => tb.HasComment("Các loại sách"));
            entity.HasKey(e => e.CategoryId);
            entity.Property(e => e.CategoryId).HasMaxLength(36);
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
