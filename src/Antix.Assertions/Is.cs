namespace Antix.Assertions;

public sealed record Is(bool Negate = false)
{
    public Is Not => new(!Negate);
}
