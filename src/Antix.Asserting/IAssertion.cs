namespace Antix.Asserting;

public interface IAssertion<in T>
{
    string Error { get; }
    string? ErrorNot { get; }
};
