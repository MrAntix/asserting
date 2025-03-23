using System.Collections.Generic;

namespace Antix.Asserting;

public sealed record CollectionItContext<TItem>(
    IReadOnlyCollection<TItem>? Collection
    ) : ItContext<CollectionItContext<TItem>, ItCollectionAdapter>(ItCollectionAdapter.From(Collection)!);