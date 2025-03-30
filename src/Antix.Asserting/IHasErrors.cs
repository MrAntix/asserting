using System.Collections.Generic;

namespace Antix.Asserting;

public interface IHasErrors
{
    IReadOnlyList<string> Errors { get; }
}
