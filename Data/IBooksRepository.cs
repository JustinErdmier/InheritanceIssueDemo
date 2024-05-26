namespace InheritanceIssueDemo.Data;

public interface IBooksRepository
{
    Task<bool> AddAsync(Book book);

    Task<Book?> GetByIdAsync(BookId bookId);

    Task<IReadOnlyList<Book>> GetAllAsync();

    Task<bool> UpdateAsync(Book book);

    Task<bool> DeleteAsync(BookId bookId);
}
