using System;
using System.Collections.Immutable;
using System.Linq;

namespace Antix.Asserting;

public static partial class Is
{
    public static Func<ItCollectionAdapter, NotNullAssertion> Any<TItem>(
        params Func<TItem, Assertion>[] providers
        ) => Count(1, null, e => $"any({e})", providers);

    public static Func<ItCollectionAdapter, NotNullAssertion> Count<TItem>(
        int count,
        params Func<TItem, Assertion>[] providers
        ) => Count(count, count, e => $"count({e})", providers);

    public static Func<ItCollectionAdapter, NotNullAssertion> MinCount<TItem>(
        int minCount,
        params Func<TItem, Assertion>[] providers
        ) => Count(minCount, null, e => $"min-count({minCount},{e})", providers);

    public static Func<ItCollectionAdapter, NotNullAssertion> MaxCount<TItem>(
        int maxCount,
        params Func<TItem, Assertion>[] providers
        ) => Count(null, maxCount, e => $"max-count({maxCount},{e}", providers);

    static Func<ItCollectionAdapter, NotNullAssertion> Count<TItem>(
        int? minCount, int? maxCount,
        Func<string, string> getFailMessage,
        params Func<TItem, Assertion>[] providers
        ) => value =>
        {
            var perItem = value.Items.Cast<TItem>()
                .Select(i => providers
                    .Select(provider => provider(i))
                    .ToImmutableArray()
                    )
                .ToImmutableArray();
            var errors = string.Join(',',
                perItem
                    .SelectMany(assertions => assertions)
                    .Select(assertion => assertion.FailMessage)
                    .Distinct()
                );

            minCount ??= 0;
            var count = perItem
                    .Count(assertions => assertions
                        .All(assertion => assertion.Success)
                        );
            var success = count >= minCount
                    && (maxCount is null || count <= maxCount.Value);

            return new()
            {
                Success = success,
                FailMessage = getFailMessage(errors)
            };
        };

    public static Func<ItCollectionAdapter, NotNullAssertion> All<TItem>(
        params Func<TItem, Assertion>[] providers
        ) => value =>
        {
            var perItem = value.Items.Cast<TItem>()
                .Select(i => providers
                    .Select(provider => provider(i))
                    .ToImmutableArray()
                    )
                .ToImmutableArray();
            var errors = string.Join(',',
                perItem
                    .SelectMany(assertions => assertions)
                    .Select(assertion => assertion.FailMessage)
                    .Distinct()
                );

            return new()
            {
                Success = perItem
                    .All(assertions => assertions
                        .All(assertion => assertion.Success)
                        ),
                FailMessage = $"all({errors})"
            };
        };

    public static Func<ItCollectionAdapter, NotNullAssertion> Count(
        int length
        ) => value => new()
        {
            Success = value.Count == length,
            FailMessage = $"count({length})"
        };

    public static Func<ItCollectionAdapter, NotNullAssertion> MinCount(
        int length
        ) => value => new()
        {
            Success = value.Count >= length,
            FailMessage = $"min-count({length})"
        };

    public static Func<ItCollectionAdapter, NotNullAssertion> MaxCount(
        int length
        ) => value => new()
        {
            Success = value.Count <= length,
            FailMessage = $"max-count({length})"
        };
}