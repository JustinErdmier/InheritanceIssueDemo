using InheritanceIssueDemo.Domain.BookAggregate.ValueObjects;
using InheritanceIssueDemo.Domain.Common.Models;

namespace InheritanceIssueDemo.Domain.BookAggregate;

public sealed class Book : AggregateRoot<BookId, Guid>
{
    private Book(string title, int numberOfPages)
        : base(id: BookId.CreateUnique())
    {
        Title         = title;
        NumberOfPages = numberOfPages;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Book()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    { }

    public string Title { get; set; }

    public int NumberOfPages { get; set; }

    public static Book Create(string title, int numberOfPages) => new(title, numberOfPages);
}
