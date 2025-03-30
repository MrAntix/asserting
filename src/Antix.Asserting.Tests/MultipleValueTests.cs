namespace Antix.Asserting.Tests;

public sealed class MultipleValueTests
{
    static readonly Person PERSON = new()
    {
        Name = "NAME"
    };

    static readonly Person?[] PEOPLE = [null, PERSON];

    [Fact]
    public void Fails()
    {
        Assert.True(
            Validate
                .It(PERSON.Name, it => it.Not.CountOf(1, 'N'))
                .It(PEOPLE, it => it.Not.CountOf(1, PERSON))
                .HasErrors(out var actualErrors)
            );

        Assert.Equal(
            [
                "PERSON.Name:not-count-of(1,'N')",
                "PEOPLE:not-count-of(1,PERSON)"
            ],
            actualErrors
            );
    }

    sealed record Person
    {
        public required string Name { get; init; }
    }
}
