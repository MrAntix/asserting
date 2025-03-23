namespace Antix.Asserting;

public sealed record ItemItContext<T>(
    T? Value
    ) : ItContext<ItemItContext<T>, T>(Value!);