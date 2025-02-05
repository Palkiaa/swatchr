﻿using UnityEngine;

// SwatchrRenderer
//  Applies a SwatchrColor in OnEnable to the connected Renderer's material.
//  Does this by setting "_Color" on the renderer's Material Property Block
namespace swatchr.components
{
    [RequireComponent(typeof(Renderer))]
    public class SwatchrRenderer : SwatchrColorApplier
    {
        [HideInInspector]
        public Renderer swatchingRenderer;

        private static MaterialPropertyBlock mpb;
        private static int colorShaderId;

        public override void Apply()
        {
            if (mpb == null)
            {
                mpb = new MaterialPropertyBlock();
                colorShaderId = Shader.PropertyToID("_Color");
            }
            if (swatchingRenderer == null)
            {
                swatchingRenderer = GetComponent<Renderer>();
            }
            mpb.SetColor(colorShaderId, swatchrColor.color);
            swatchingRenderer.SetPropertyBlock(mpb);
        }
    }
}