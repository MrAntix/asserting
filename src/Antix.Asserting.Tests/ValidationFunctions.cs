namespace Antix.Asserting.Tests;

public sealed class ValidationFunctions
{
    static readonly Person PERSON = new()
    {
        Name = "NAME",
        Age = 45
    };

    [Fact]
    public void Happy()
        => Assert.Empty(
            Validate
                .It(PERSON, Person.Valid)
                .Errors
            );

    [Fact]
    public void Fails()
    {
        var person = new Person()
        {
            Name = null!,
            Age = 1
        };

        Assert.True(
            Validate
                .It(person, Person.Valid)
                .HasErrors(out var errors)
            );

        Assert.Equal(
            ["person.Name:not-null"],
            errors
            );
    }

    [Fact]
    public void Fails_Not()
    {
        var person = new Person()
        {
            Name = null!,
            Age = 1
        };

        Assert.Empty(
            Validate
                .It(person, @is => Person.Valid(@is.Not))
                .Errors
            );
    }

    sealed record Person : HasName
    {
        public required int Age { get; init; }

        public static bool Valid(
            IValidate<Person> it
            ) => it.Is(HasName.Valid)
                .It(v => v.Age, @is => @is.GreaterOrEqualTo(0), nameof(Age))
                .Ok();
    }

    abstract record HasName
    {
        public required string Name { get; init; }

        public static bool Valid(
            IValidate<HasName> it
            ) => it
                .It(v => v.Name, @is => @is.Not.Null() && @is.LengthGreaterOrEqualTo(2), nameof(Name))
                .Ok();
    }
}