using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

// SwatchrButton
//  Applies a SwatchrColor in OnEnable to the connected Renderer's material.
//  Does this by setting "_Color" on the renderer's Material Property Block
namespace swatchr.components
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Image))]
    public class SwatchrImage : MonoBehaviour, ISwatchrColorApplier
    {
        public SwatchrColor imageColor;

        [HideInInspector]
        public Image image;

        private void OnDestroy()
        {
            imageColor.OnColorChanged -= Apply;
        }

        private void OnDisable()
        {
            imageColor.OnColorChanged -= Apply;
        }

        private void OnEnable()
        {
            if (imageColor == null) imageColor = new SwatchrColor();
            imageColor.OnColorChanged += Apply;
            imageColor.OnEnable();
        }

        public IEnumerable<Guid> ColorsUsed()
        {
            return new Guid[] { imageColor.colorId };
        }

        public void Apply()
        {
            if (image == null)
                image = GetComponent<Image>();

            image.color = imageColor.color;
        }
    }
}