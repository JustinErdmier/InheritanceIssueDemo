using InheritanceIssueDemo.Domain.BookAggregate;
using InheritanceIssueDemo.Domain.BookAggregate.ValueObjects;

namespace InheritanceIssueDemo.Data;

public sealed class BooksRepository : IBooksRepository
{
    private readonly List<Book> _books =
    [
        Book.Create(title: "Winnie ther Pooh", numberOfPages: 125), Book.Create(title: "Pro ASP.NET Core Identity", numberOfPages: 725),
        Book.Create(title: "Pro ASP.NET Core MVC 2", numberOfPages: 725), Book.Create(title: "C# 10 in a Nutshell", numberOfPages: 568)
    ];

    /// <inheritdoc />
    public async Task<bool> AddAsync(Book book)
    {
        await SimulateDelay();

        if (_books.Contains(book))
        {
            return false;
        }

        _books.Add(book);

        return true;
    }

    /// <inheritdoc />
    public async Task<Book?> GetByIdAsync(BookId bookId)
    {
        await SimulateDelay();

        return _books.FirstOrDefault(book => book.Id == bookId);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Book>> GetAllAsync()
    {
        await SimulateDelay();

        return _books.AsReadOnly();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Book book)
    {
        await SimulateDelay();

        Book? existingBook = _books.FirstOrDefault(b => b.Id == book.Id);

        if (existingBook is null)
        {
            return false;
        }

        // Just for demo purposes
        _books.Remove(existingBook);
        _books.Add(book);

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(BookId bookId)
    {
        await SimulateDelay();

        Book? book = _books.FirstOrDefault(b => b.Id == bookId);

        if (book is null)
        {
            return false;
        }

        _books.Remove(book);

        return true;
    }

    private static async Task SimulateDelay() => await Task.Delay(delay: TimeSpan.FromSeconds(value: 8));
}
