using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

// SwatchrText
//  Applies a SwatchrColor in OnEnable to the connected Renderer's material.
//  Does this by setting "_Color" on the renderer's Material Property Block
namespace swatchr.components
{
    [RequireComponent(typeof(Text))]
    public class SwatchrText : MonoBehaviour, ISwatchrColorApplier
    {
        public SwatchrColor textColor;

        [HideInInspector]
        public Text text;

        public IEnumerable<Guid> ColorsUsed()
        {
            return new Guid[] { textColor.colorId };
        }

        public void Apply()
        {
            if (text == null)
                text = GetComponentInChildren<Text>();

            text.color = textColor.color;
        }
    }
}