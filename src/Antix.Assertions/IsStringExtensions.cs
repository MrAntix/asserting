using System;
using System.Linq;

namespace Antix.Assertions;

public static class IsStringExtensions
{
    public static Assertion<string> Empty(
        this Is @is,
        string? value
        ) => new()
        {
            Test = () => value?.Length == 0,
            Negate = @is.Negate,
            FailMessage = "empty"
        };

    public static Assertion<string> Whitespace(
        this Is @is,
        string? value
        ) => new()
        {
            Test = () => value?.Length > 0 && value.All(char.IsWhiteSpace),
            Negate = @is.Negate,
            FailMessage = "whitespace"
        };

    public static Func<string?, Assertion<string>> Length(
        this Is @is,
        int length
        ) => value => new()
        {
            Test = () => value?.Length == length,
            Negate = @is.Negate,
            FailMessage = $"length({length})"
        };

    public static Func<string?, Assertion<string>> MinLength(
        this Is @is,
        int length
        ) => value => new()
        {
            Test = () => value?.Length >= length,
            Negate = @is.Negate,
            FailMessage = $"min-length({length})"
        };

    public static Func<string?, Assertion<string>> MaxLength(
        this Is @is,
        int length
        ) => value => new()
        {
            Test = () => value?.Length <= length,
            Negate = @is.Negate,
            FailMessage = $"max-length({length})"
        };
}
