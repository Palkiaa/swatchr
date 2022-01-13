using System;

using UnityEngine;

namespace swatchr
{
    public static class SwatchImporting
    {
        public static Swatch FromSwatchASEFile(SwatchASEFile file)
        {
            Swatch swatchScriptableObject = ScriptableObject.CreateInstance<Swatch>();

            swatchScriptableObject.Clear();
            for (int i = 0; i < file.colors.Count; i++)
            {
                swatchScriptableObject.Add(Guid.NewGuid(), new Color(file.colors[i].r, file.colors[i].g, file.colors[i].b));
            }

            return swatchScriptableObject;
        }

        public static void AddColorsFromASEFile(this Swatch swatch, SwatchASEFile file)
        {
            var iterator = file.colors.GetEnumerator();
            while (iterator.MoveNext())
            {
                var fileColor = iterator.Current;
                swatch.Add(Guid.NewGuid(), new Color(fileColor.r, fileColor.g, fileColor.b));
            }
            swatch.SignalChange();
        }

        public static void AddColorsFromOtherSwatch(this Swatch swatch, Swatch otherSwatch, bool replaceExisting = false)
        {
            foreach (var item in otherSwatch)
            {
                if (replaceExisting)
                {
                    swatch[item.Key] = item.Value;
                }
                else
                {
                    swatch.Add(Guid.NewGuid(), item.Value);
                }
                
            }
            swatch.SignalChange();
        }

        public static void ReplaceSelfWithOtherSwatch(this Swatch swatch, Swatch otherSwatch)
        {
            swatch.Clear();
            swatch.AddColorsFromOtherSwatch(otherSwatch);
            swatch.SignalChange();
        }
    }
}