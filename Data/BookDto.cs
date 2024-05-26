using InheritanceIssueDemo.Domain.BookAggregate;
using InheritanceIssueDemo.Domain.BookAggregate.ValueObjects;

namespace InheritanceIssueDemo.Data;

public sealed record BookDto(string Title, int NumberOfPages, Guid Id)
{
    public static BookDto Create(string title, int numberOfPages, Guid id) => new(title, numberOfPages, id);
}

public static class BookExtensions
{
    public static Book ToBook(this BookDto dto) => Book.Create(dto.Title, dto.NumberOfPages, id: BookId.Create(dto.Id));

    public static BookDto ToDto(this Book book) => BookDto.Create(book.Title, book.NumberOfPages, book.Id.Value);

    public static IList<Book> ToBooks(this IEnumerable<BookDto> dtos) =>
        dtos.Select(ToBook)
            .ToList();

    public static List<BookDto> ToDtos(this IEnumerable<Book> books) =>
        books.Select(ToDto)
             .ToList();
}
