using System;
using System.Collections.Generic;

using UnityEngine;

namespace swatchr.components
{

    [ExecuteInEditMode]
    public abstract class SwatchrColorApplier : MonoBehaviour, ISwatchrColorApplier
    {
        public SwatchrColor swatchrColor;

        private void OnDestroy()
        {
            swatchrColor.OnColorChanged -= Apply;
        }

        private void OnDisable()
        {
            swatchrColor.OnColorChanged -= Apply;
        }

        private void OnEnable()
        {
            if (swatchrColor == null)
            {
                swatchrColor = new SwatchrColor();
            }
            swatchrColor.OnColorChanged += Apply;
            swatchrColor.OnEnable();
        }

        public virtual IEnumerable<Guid> ColorsUsed()
        {
            return new Guid[] { swatchrColor.colorId };
        }

        public abstract void Apply();


    }
}