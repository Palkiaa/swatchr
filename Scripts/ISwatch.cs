using System;
using System.Collections.Generic;

using UnityEngine;

namespace swatchr
{
    public interface ISwatch<TKey, TValue> : IDictionary<TKey, TValue>
    {
        Texture2D cachedTexture { get; }

        event EventHandler OnChanged;

        void SignalChange();
    }
}