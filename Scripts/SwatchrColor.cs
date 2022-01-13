using System;

using UnityEngine;

using Guid = SerializableGuid;

namespace swatchr
{
    // SwatchrColor
    //  Holds a Swatch and an index into that Swatch.
    //  Returns a color using the color index and the swatch.
    //  Implements the "Observer Pattern" allowing classes to listen
    //  to changes in the swatch or colorIndex.
    //  Calls OnColorChanged when the swatch or the colorIndex has changed.
    //  In order to react to changes within the swatch, we need to
    //  subscribe to OnSwatchChanged events.
    //  We use a "weak delegate" pattern so that we never have to
    //  un register for the event. This is useful because this class
    //  cannot simply deregister in the destructor because the destructor
    //  won't get called if we use a regular event subscription pattern.
    //  More info on weak delegates can be found here:
    //  https://www.codeproject.com/Articles/29922/Weak-Events-in-C#heading0002
    [Serializable]
    public class SwatchrColor
    {
        public SwatchrColor()
        {
        }

        public void OnEnable()
        {
            swatch = _swatch; // this will subscribe to changes in the swatch, and call OnColorChanged
        }

        public void OnDisable()
        {
            if (_swatch != null)
                _swatch.OnChanged -= OnSwatchChanged;
        }

        private void OnSwatchChanged(object sender, EventArgs e)
        {
            if (OnColorChanged != null)
                OnColorChanged();
        }

        public Swatch swatch
        {
            get { return _swatch; }
            set
            {
                if (_swatch != null)
                    _swatch.OnChanged -= OnSwatchChanged;
                _swatch = value;

                if (_swatch != null)
                    _swatch.OnChanged += OnSwatchChanged;
                if (OnColorChanged != null)
                    OnColorChanged();
            }
        }

        public Guid colorId
        {
            get { return _colorId; }
            set
            {
                _colorId = value;
                if (OnColorChanged != null) OnColorChanged();
            }
        }

        public Color color
        {
            get
            {
                if (swatch != null && swatch.TryGetValue(colorId, out var color))
                {
                    return color;
                }
                return _overrideColor;
            }
        }

        [SerializeField]
        public Swatch _swatch;

        [SerializeField]
        public Guid _colorId;

        [SerializeField]
        public Color _overrideColor;

        public event Action OnColorChanged;
    }
}