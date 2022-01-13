using System;
using System.Collections.Generic;

namespace swatchr.components
{
    public interface ISwatchrColorApplier
    {
        IEnumerable<Guid> ColorsUsed();

        void Apply();
    }
}