using System;
using System.Linq;

namespace Antix.Asserting;

public sealed record ItContext<T> : ItContextBase
{
    public T? Value { get; init; }

    public ItContext<T> Not => this with { Negate = !Negate };

    public ItContext<T> Is(
        params Func<ItContext<T>, ItContext<T>>[] assertions
        ) => assertions.Aggregate(this, (vc, assertion) => assertion(vc)) with
        {
            Negate = false
        };

    public ItContext<T> And(
        Func<T?, Assertion> provider
        ) => this with
        {
            _assertions = _assertions.Add(() =>
            {
                var assertion = provider(Value);
                var error = assertion.Run(Negate);

                return error is null ? null : $"{Expression}:{error}";
            })
        };
}
