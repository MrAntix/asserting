using System.Collections;
using System.Collections.Generic;

namespace Antix.Asserting;

public sealed record ItCollectionAdapter
{
    public required int Count { get; init; }
    public required IEnumerable Items { get; init; }

    public static ItCollectionAdapter? From<TItem>(IReadOnlyCollection<TItem>? collection)
        => collection is null ? null : new()
        {
            Count = collection.Count,
            Items = collection
        };
}
