using UnityEngine;

namespace swatchr.components
{
    [RequireComponent(typeof(Light))]
    public class SwatchrLight : SwatchrColorApplier
    {
        private Light swatchingLight;

        public override void Apply()
        {
            if (swatchingLight == null)
            {
                swatchingLight = GetComponent<Light>();
            }
            swatchingLight.color = swatchrColor.color;
        }
    }
}