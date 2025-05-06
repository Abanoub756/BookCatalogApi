using BookCatalogApi.Models;
using BookCatalogApi.Repositories;
using Xunit;

public class BookTests
{
    [Fact]
    public void CanAddBook()
    {
        var book = new Book { Title = "Test", Author = "Me", Genre = "Tech", PageCount = 123 };
        var added = BookRepository.Add(book);
        Assert.NotEqual(0, added.BookId);
    }

    [Fact]
    public void SearchReturnsCorrectBook()
    {
        BookRepository.Add(new Book { Title = "DotNet Guide", Author = "John", Genre = "Tech", PageCount = 200 });
        var result = BookRepository.Search("dotnet", null);
        Assert.Single(result);
    }

    [Fact]
    public void UpdateReturnsFalseIfNotFound()
    {
        var updated = new Book { Title = "Nope", Author = "Nobody", Genre = "None", PageCount = 0 };
        Assert.False(BookRepository.Update(-1, updated));
    }
}
