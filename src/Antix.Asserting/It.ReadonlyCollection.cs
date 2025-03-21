using System;

namespace Antix.Asserting;

public static partial class Is
{
    public static Func<IItCollectionContext, IItCollectionContext> Count(
        int length
        )
        => context => context.And(
            value => new()
            {
                TestSuccess = () => value?.Count == length,
                FailMessage = $"count({length})"
            });

    public static Func<IItCollectionContext, IItCollectionContext> MinCount(
        int length
        )
        => context => context.And(
            value => new()
            {
                TestSuccess = () => value?.Count >= length,
                FailMessage = $"min-count({length})"
            });

    public static Func<IItCollectionContext, IItCollectionContext> MaxCount(
        int length
        )
        => context => context.And(
            value => new()
            {
                TestSuccess = () => value?.Count <= length,
                FailMessage = $"max-count({length})"
            });
}