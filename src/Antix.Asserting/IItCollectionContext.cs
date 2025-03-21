using System;

namespace Antix.Asserting;

public interface IItCollectionContext
{
    IItCollectionContext And(
       Func<ItCollectionAdapter?, Assertion> provider
       );
}
