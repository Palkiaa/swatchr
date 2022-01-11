using System;

using UnityEngine;

namespace swatchr
{
    public static class SwatchImporting
    {
        public static Swatch FromSwatchASEFile(SwatchASEFile file)
        {
            var swatchScriptableObject = ScriptableObject.CreateInstance<Swatch>();
            swatchScriptableObject.colors = new Color[file.colors.Count];
            for (int i = 0; i < swatchScriptableObject.colors.Length; i++)
            {
                swatchScriptableObject.colors[i] = new Color(file.colors[i].r, file.colors[i].g, file.colors[i].b);
            }
            return swatchScriptableObject;
        }

        public static void AddColorsFromASEFile(this Swatch swatch, SwatchASEFile file)
        {
            int initialLength = swatch.colors != null ? swatch.colors.Length : 0;
            int fileLength = file.colors != null ? file.colors.Count : 0;
            Array.Resize(ref swatch.colors, initialLength + fileLength);
            int i = initialLength;
            var iterator = file.colors.GetEnumerator();
            while (iterator.MoveNext())
            {
                var fileColor = iterator.Current;
                swatch.colors[i++] = new Color(fileColor.r, fileColor.g, fileColor.b);
            }
            swatch.SignalChange();
        }

        public static void AddColorsFromOtherSwatch(this Swatch swatch, Swatch otherSwatch)
        {
            int initialLength = swatch.colors != null ? swatch.colors.Length : 0;
            int otherSwatchLength = otherSwatch.colors != null ? otherSwatch.colors.Length : 0;
            Array.Resize(ref swatch.colors, initialLength + otherSwatchLength);
            int i = initialLength;
            for (int j = 0; j < otherSwatchLength; j++)
            {
                swatch.colors[i++] = otherSwatch.colors[j];
            }
            swatch.SignalChange();
        }

        public static void ReplaceSelfWithOtherSwatch(this Swatch swatch, Swatch otherSwatch)
        {
            if (otherSwatch.colors != null)
            {
                Array.Resize(ref swatch.colors, otherSwatch.colors.Length);
                Array.Copy(otherSwatch.colors, swatch.colors, otherSwatch.colors.Length);
            }
            else
            {
                Array.Resize(ref swatch.colors, 0);
            }
            swatch.SignalChange();
        }
    }
}