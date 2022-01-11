using System;

using UnityEngine;

//https://www.codeproject.com/Articles/29922/Weak-Events-in-C#heading0002
//https://gist.github.com/dgrunwald/6445360
namespace swatchr
{
    public class Swatch : ScriptableObject
    {
        public Color[] colors;
        public int numColors
        { get { return colors != null ? colors.Length : 0; } }

        [NonSerialized]
        private Texture2D texture = null;

        public Texture2D cachedTexture
        {
            get
            {
                if (texture == null)
                {
                    texture = CreateTexture();
                }
                return texture;
            }
        }

        public void RegenerateTexture()
        {
            texture = CreateTexture();
        }

        private Texture2D CreateTexture()
        {
#if SWATCHR_VERBOSE
			Debug.LogWarning("[Swatch] Creating Texture");
#endif

            var swatch = this;
            if (swatch.colors != null && swatch.colors.Length > 0)
            {
                Texture2D colorTexture = new Texture2D(swatch.colors.Length, 1);
                colorTexture.filterMode = FilterMode.Point;
                colorTexture.SetPixels(swatch.colors);
                colorTexture.Apply();
                return colorTexture;
            }
            return null;
        }

        public event EventHandler OnSwatchChanged
        {
            //add { _event.Add(value); }
            //remove { _event.Remove(value); }
            add { _event += value; }
            remove { _event -= value; }
        }

        [NonSerialized]
        private EventHandler _event;

        public Color GetColor(int colorIndex)
        {
            if (colors == null || colors.Length <= colorIndex || colorIndex < 0)
            {
                return Color.white;
            }
            return colors[colorIndex];
        }

        public void SignalChange()
        {
            RegenerateTexture();
            //_event.Raise(this, EventArgs.Empty);
            _event.Invoke(this, EventArgs.Empty);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}