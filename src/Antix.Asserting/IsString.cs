using System.Linq;

namespace Antix.Asserting;

public static class IsString
{
    public static bool Whitespace(
        this IValidate<string> context
        ) => context.AssertNotNull(
            value => value.Length > 0 && value.All(char.IsWhiteSpace),
            "whitespace"
        );

    public static bool StartsWith(
        this IValidate<string> context,
        string text
        ) => context.AssertNotNull(
            value => value.StartsWith(text),
            $"starts-with({text})"
        );

    public static bool EndsWith(
        this IValidate<string> context,
        string text
        ) => context.AssertNotNull(
            value => value.EndsWith(text),
            $"ends-with({text})"
        );

    public static bool Contains(
        this IValidate<string> context,
        string text
        ) => context.AssertNotNull(
            value => value.Contains(text),
            $"contains({text})"
        );
}
