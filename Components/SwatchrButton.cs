using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

// SwatchrButton
//  Applies a SwatchrColor in OnEnable to the connected Renderer's material.
//  Does this by setting "_Color" on the renderer's Material Property Block
namespace swatchr.components
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class SwatchrButton : MonoBehaviour, ISwatchrColorApplier
    {
        [Header("Background")]
        public SwatchrColor imageColor;

        [Space]
        [Header("Button")]
        public bool ApplyButtonColors = false;

        public SwatchrColor normalColor;
        public SwatchrColor highlightedColor;
        public SwatchrColor pressedColor;
        public SwatchrColor selectedColor;
        public SwatchrColor disabledColor;

        [Space]
        public bool ApplyTextColor = false;

        public SwatchrColor textColor;

        [HideInInspector]
        public Image image;

        [HideInInspector]
        public Text text;

        [HideInInspector]
        public Button button;

        public IEnumerable<Guid> ColorsUsed()
        {
            List<Guid> colors = new List<Guid>();
            colors.Add(imageColor.colorId);
            if (ApplyButtonColors)
            {
                colors.Add(normalColor.colorId);
                colors.Add(highlightedColor.colorId);
                colors.Add(pressedColor.colorId);
                colors.Add(selectedColor.colorId);
                colors.Add(disabledColor.colorId);
            }
            if (ApplyTextColor)
            {
                colors.Add(textColor.colorId);
            }
            return colors;
        }

        public void Apply()
        {
            try
            {
                if (button == null)
                    button = GetComponent<Button>();

                if (image == null)
                    image = GetComponent<Image>();

                if (text == null)
                    text = GetComponentInChildren<Text>();

                image.color = imageColor.color;

                if (ApplyButtonColors)
                {
                    var colorBlock = new ColorBlock()
                    {
                        normalColor = normalColor.color,
                        highlightedColor = highlightedColor.color,
                        pressedColor = pressedColor.color,
                        selectedColor = pressedColor.color,
                        disabledColor = disabledColor.color,
                        colorMultiplier = button.colors.colorMultiplier,
                        fadeDuration = button.colors.fadeDuration
                    };
                    button.colors = colorBlock;
                }
                if (ApplyTextColor)
                {
                    text.color = textColor.color;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}