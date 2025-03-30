using System.Collections.Generic;
using System.Linq;

namespace Antix.Asserting.Tests;

public sealed class IsEnumerableTests
{
    static readonly Person PERSON_1 = new()
    {
        Name = "PERSON_1_NAME",
        Age = 35
    };
    static readonly Person PERSON_2 = new()
    {
        Name = "PERSON_2_NAME",
        Age = 45
    };
    static readonly Person PERSON_3 = new()
    {
        Name = "PERSON_3_NAME",
        Age = 55
    };

    static readonly Person?[] PEOPLE = [null, PERSON_1, PERSON_2];

    static NotNullAssertion<Person> OLDER_THAN(int age)
        => new(p => p.Age > age, $"older-than({age})");

    static NotNullAssertion<IEnumerable<Person?>> TOTAL_AGE(int value)
        => new(p => p.Sum(i => i?.Age ?? 0) == value, $"total-age({value})");

    [Fact]
    public void Happy() =>
        Assert.Empty(
            Validate
                .It(PEOPLE, it =>
                    it.Length(3) && it.LengthGreaterOrEqualTo(3) && it.LengthLessOrEqualTo(3)
                    && it.CountOf(1, PERSON_1)
                    && it.CountOf(2, OLDER_THAN(30))
                    && it.Assert(TOTAL_AGE(PERSON_1.Age + PERSON_2.Age))
                    )
                .Errors
        );

    [Fact]
    public void Fail_LengthGreaterOrEqualTo()
    {
        Assert.True(
            Validate
                .It(PEOPLE,
                    it => it.LengthGreaterOrEqualTo(100)
                    )
                .HasError(out var actualError)
            );

        Assert.Equal("PEOPLE:length-greater-or-equal-to(100)", actualError);
    }

    [Fact]
    public void Fail_LengthLessOrEqualTo()
    {
        Assert.True(
            Validate
                .It(PEOPLE,
                    it => it.LengthLessOrEqualTo(0)
                    )
                .HasError(out var actualError)
            );

        Assert.Equal("PEOPLE:length-less-or-equal-to(0)", actualError);
    }

    [Fact]
    public void Fail_Is_Assertion()
    {
        Assert.True(
            Validate
                .It(PEOPLE,
                    it => it.Assert(TOTAL_AGE(0))
                    )
                .HasError(out var actualError)
            );

        Assert.Equal("PEOPLE:total-age(0)", actualError);

    }

    [Fact]
    public void Fail_CountOf_Value()
    {
        Assert.True(
            Validate
                .It(PEOPLE,
                    it => it.CountOf(2, PERSON_3)
                    )
                .HasError(out var actualError)
            );

        Assert.Equal("PEOPLE:count-of(2,PERSON_3)", actualError);
    }

    [Fact]
    public void Fail_CountOfMin_Value()
    {
        Assert.True(
            Validate
                .It(PEOPLE,
                    it => it.CountOfMin(2, PERSON_3)
                    )
                .HasError(out var actualError)
            );

        Assert.Equal("PEOPLE:count-of-min(2,PERSON_3)", actualError);
    }

    [Fact]
    public void Fail_CountOfMax_Value()
    {
        Assert.True(
            Validate
                .It(PEOPLE,
                    it => it.CountOfMax(0, PERSON_1)
                    )
                .HasError(out var actualError)
            );

        Assert.Equal("PEOPLE:count-of-max(0,PERSON_1)", actualError);
    }

    [Fact]
    public void Fail_CountOfMinMax_Value()
    {
        Assert.True(
            Validate
                .It(PEOPLE,
                    it => it.CountOfMinMax(2, 0, PERSON_1)
                    )
                .HasError(out var actualError)
            );

        Assert.Equal("PEOPLE:count-of-min-max(2,0,PERSON_1)", actualError);
    }

    [Fact]
    public void Fail_CountOf_Assertion()
    {
        Assert.True(
            Validate
                .It(PEOPLE,
                    it => it.CountOf(2, OLDER_THAN(65))
                    )
                .HasError(out var actualError)
            );

        Assert.Equal("PEOPLE:count-of(2,older-than(65))", actualError);
    }

    [Fact]
    public void Fail_MinCountOf_Assertion()
    {
        Assert.True(
            Validate
                .It(PEOPLE,
                    it => it.CountOfMin(2, OLDER_THAN(65))
                    )
                .HasError(out var actualError)
            );

        Assert.Equal("PEOPLE:count-of-min(2,older-than(65))", actualError);
    }

    [Fact]
    public void Fail_CountOfMax_Assertion()
    {
        Assert.True(
            Validate
                .It(PEOPLE,
                    it => it.CountOfMax(0, OLDER_THAN(25))
                    )
                .HasError(out var actualError)
            );

        Assert.Equal("PEOPLE:count-of-max(0,older-than(25))", actualError);
    }

    [Fact]
    public void Fail_CountOfMinMax_Assertion()
    {
        Assert.True(
            Validate
                .It(PEOPLE,
                    it => it.CountOfMinMax(2, 0, OLDER_THAN(65))
                    )
                .HasError(out var actualError)
            );

        Assert.Equal("PEOPLE:count-of-min-max(2,0,older-than(65))", actualError);
    }

    sealed record Person
    {
        public required string Name { get; init; }
        public required int Age { get; init; }
    }
}