using BookCatalogApi.Models;
using System.Xml.Linq;

namespace BookCatalogApi.Repositories
{
    public static class BookRepository
    {
        private static List<Book> _books = new();
        private static int _nextId = 1;

        public static List<Book> GetAll() => _books;

        public static Book? GetById(int id) => _books.FirstOrDefault(b => b.BookId == id);

        public static Book Add(Book book)
        {
            book.BookId = _nextId++;
            _books.Add(book);
            return book;
        }

        public static bool Update(int id, Book updated)
        {
            var book = GetById(id);
            if (book == null) return false;

            book.Title = updated.Title;
            book.Author = updated.Author;
            book.Genre = updated.Genre;
            book.PageCount = updated.PageCount;
            return true;
        }

        public static bool Delete(int id)
        {
            var book = GetById(id);
            if (book == null) return false;

            _books.Remove(book);
            return true;
        }

        public static List<Book> Search(string? title, string? author)
        {
            return _books.Where(b =>
                (string.IsNullOrEmpty(title) || b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(author) || b.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }
    }

}
