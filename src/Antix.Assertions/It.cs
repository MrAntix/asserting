using System.Runtime.CompilerServices;

namespace Antix.Assertions;

public static class It
{
    public static Is Is { get; } = new();
    public static ValueContext<T> For<T>(
        T? value,
        [CallerArgumentExpression(nameof(value))] string? caller = null
        ) => new(caller!, value);
}
