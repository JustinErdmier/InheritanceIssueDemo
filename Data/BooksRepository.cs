namespace InheritanceIssueDemo.Data;

public sealed class BooksRepository : IBooksRepository
{
    private readonly AppDbContext _context;

    public BooksRepository(AppDbContext context) => _context = context;

    /// <inheritdoc />
    public async Task<bool> AddAsync(Book book)
    {
        await SimulateDelay();

        if (_context.Books.Contains(book))
        {
            return false;
        }

        _context.Books.Add(book);

        await _context.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<Book?> GetByIdAsync(BookId bookId)
    {
        await SimulateDelay();

        return await _context.Books.FirstOrDefaultAsync(book => book.Id == bookId);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Book>> GetAllAsync()
    {
        await SimulateDelay();

        return (await _context.Books.ToListAsync()).AsReadOnly();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Book book)
    {
        await SimulateDelay();

        Book? existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);

        if (existingBook is null)
        {
            return false;
        }

        // Just for demo purposes
        _context.Books.Remove(existingBook);
        _context.Books.Add(book);

        await _context.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(BookId bookId)
    {
        await SimulateDelay();

        Book? book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);

        if (book is null)
        {
            return false;
        }

        _context.Books.Remove(book);

        await _context.SaveChangesAsync();

        return true;
    }

    private static async Task SimulateDelay() => await Task.Delay(delay: TimeSpan.FromSeconds(value: 8));
}
