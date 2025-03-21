using System;
using System.Linq;

namespace Antix.Asserting;

public static partial class Is
{
    public static ItContext<string> Empty(
        ItContext<string> context
        ) => context.And(
            value => new()
            {
                TestSuccess = () => value?.Length == 0,
                FailMessage = "empty"
            });

    public static ItContext<string> Whitespace(
        ItContext<string> context
        ) => context.And(
            value => new()
            {
                TestSuccess = () => value?.Length > 0 && value.All(char.IsWhiteSpace),
                FailMessage = "whitespace"
            });

    public static Func<ItContext<string>, ItContext<string>> Length(
        int length
        ) => context => context.And(
            value => new()
            {
                TestSuccess = () => value?.Length == length,
                FailMessage = $"length({length})"
            });

    public static Func<ItContext<string>, ItContext<string>> MinLength(
        int length
        ) => context => context.And(
            value => new()
            {
                TestSuccess = () => value?.Length >= length,
                FailMessage = $"min-length({length})"
            });

    public static Func<ItContext<string>, ItContext<string>> MaxLength(
        int length
        ) => context => context.And(
            value => new()
            {
                TestSuccess = () => value?.Length <= length,
                FailMessage = $"max-length({length})"
            });
}
