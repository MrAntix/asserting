using System;
using System.Collections.Generic;
using System.Linq;

namespace Antix.Asserting;

public sealed record ItCollectionContext<TItem> :
    ItContextBase,
    IItCollectionContext
{
    public IReadOnlyCollection<TItem?>? Value { get; init; }

    public ItCollectionContext<TItem> Not => this with { Negate = !Negate };

    public ItCollectionContext<TItem> Is(
        params Func<IItCollectionContext, IItCollectionContext>[] assertions
        ) => assertions.Aggregate(this, (vc, assertion) => (ItCollectionContext<TItem>)assertion(vc)) with
        {
            Negate = false
        };

    public ItCollectionContext<TItem> Contains(
        Func<TItem?, bool> predicate
        ) => And(
            value => new()
            {
                TestSuccess = () => value?.Any(predicate) == true,
                FailMessage = $"contains"
            }) with
        {
            Negate = false
        };

    public ItCollectionContext<TItem> Contains(
        TItem? item
        ) => Contains(i => Equals(i, item));

    public ItCollectionContext<TItem> And(
        Func<IReadOnlyCollection<TItem?>?, Assertion> provider
        ) => this with
        {
            _assertions = _assertions.Add(() =>
            {
                var assertion = provider(Value);
                var error = assertion.Run(Negate);

                return error is null ? null : $"{Expression}:{error}";
            })
        };

    IItCollectionContext IItCollectionContext.And(
        Func<ItCollectionAdapter?, Assertion> provider
        ) => this with
        {
            _assertions = _assertions.Add(() =>
            {
                var assertion = provider(Value is null
                    ? null
                    : new ItCollectionAdapter
                    {
                        Count = Value.Count
                    });
                var error = assertion.Run(Negate);

                return error is null ? null : $"{Expression}:{error}";
            })
        };
}
