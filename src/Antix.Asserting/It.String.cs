using System;
using System.Linq;

namespace Antix.Asserting;

public static partial class Is
{
    public static NotNullAssertion Whitespace(
        string value
        ) => new()
        {
            Success = value.Length > 0 && value.All(char.IsWhiteSpace),
            FailMessage = "whitespace"
        };

    public static NotNullAssertion NotWhitespace(
        string value
        ) => new()
        {
            Success = value.Length == 0 || !value.All(char.IsWhiteSpace),
            FailMessage = "not-whitespace",
            FailNotMessage = "whitespace"
        };

    public static Func<string, NotNullAssertion> Length(
        int length
        ) => value => new()
        {
            Success = value.Length == length,
            FailMessage = $"length({length})"
        };

    public static Func<string, NotNullAssertion> MinLength(
        int length
        ) => value => new()
        {
            Success = value.Length >= length,
            FailMessage = $"min-length({length})"
        };

    public static Func<string, NotNullAssertion> MaxLength(
        int length
        ) => value => new()
        {
            Success = value.Length <= length,
            FailMessage = $"max-length({length})"
        };
}
