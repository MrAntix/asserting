using System;

namespace Antix.Asserting;

public static class AssertionExtensions
{
    public static string? Run<TValue>(
        this Func<TValue, Assertion> provider,
        TValue value, bool negate
        ) => value is null
                && provider.Method.ReturnType == typeof(NotNullAssertion)
            ? null
            : provider(value!).Run(negate);

    public static string? Run(
        this Assertion assertion,
        bool negate) =>
        GetMessage(
            assertion.Success, negate,
            assertion.FailMessage, assertion.FailNotMessage
            );

    public static string? GetMessage(
        bool success, bool negate,
        string failMessage, string? failNotMessage = null
        ) => success != negate
            ? null
            : negate
                ? failNotMessage ?? $"not-{failMessage}"
                : failMessage;
}