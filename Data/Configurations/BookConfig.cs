using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InheritanceIssueDemo.Data.Configurations;

public sealed class BookConfig : IEntityTypeConfiguration<Book>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable(name: "Books");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
               .ValueGeneratedNever()
               .HasConversion(id => id.Value, value => BookId.Create(value));
    }
}
