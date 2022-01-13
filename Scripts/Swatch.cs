using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Guid = SerializableGuid;

//https://www.codeproject.com/Articles/29922/Weak-Events-in-C#heading0002
//https://gist.github.com/dgrunwald/6445360
namespace swatchr
{
    public class Swatch : ScriptableObject, ISwatch<Guid, Color>
    {
        public Color defaultColor => Color.white;

        [SerializeField]
        private SerializableDictionary<Guid, Color> colors;

        public event EventHandler OnChanged
        {
            //add { _event.Add(value); }
            //remove { _event.Remove(value); }
            add { _event += value; }
            remove { _event -= value; }
        }

        [NonSerialized]
        private EventHandler _event;

        public Swatch()
        {
            colors = new SerializableDictionary<Guid, Color>();
        }

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
            try
            {
                if (swatch.colors != null && 0 < swatch.colors.Count)
                {
                    Texture2D colorTexture = new Texture2D(swatch.colors.Count, 1);
                    colorTexture.filterMode = FilterMode.Point;
                    Color[] pixels = new Color[swatch.Count];
                    int i = 0;
                    foreach (var item in swatch)
                    {
                        pixels[i++] = item.Value;
                    }
                    //var tempColors = swatch.Values.ToArray();
                    colorTexture.SetPixels(pixels);
                    colorTexture.Apply();
                    return colorTexture;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public void SignalChange()
        {
            RegenerateTexture();
            //_event.Raise(this, EventArgs.Empty);
            _event?.Invoke(this, EventArgs.Empty);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public ICollection<Guid> Keys => colors.Keys;

        public ICollection<Color> Values => colors.Values;

        public int Count => colors.Count;

        public bool IsReadOnly => false;

        public Color this[Guid key]
        {
            get => colors[key];
            set => colors[key] = value;
        }

        public void Add(Guid key, Color value)
        {
            colors.Add(key, value);
        }

        public bool ContainsKey(Guid key)
        {
            return colors.ContainsKey(key);
        }

        public bool Remove(Guid key)
        {
            return colors.Remove(key);
        }

        public bool TryGetValue(Guid key, out Color value)
        {
            return colors.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<Guid, Color> item)
        {
            colors.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            colors.Clear();
        }

        public bool Contains(KeyValuePair<Guid, Color> item)
        {
            return colors.Contains(item);
        }

        public void CopyTo(KeyValuePair<Guid, Color>[] array, int arrayIndex)
        {
        }

        public bool Remove(KeyValuePair<Guid, Color> item)
        {
            return colors.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<Guid, Color>> GetEnumerator()
        {
            return colors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return colors.GetEnumerator();
        }
    }
}