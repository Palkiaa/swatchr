using UnityEngine;

namespace swatchr.components
{
    [ExecuteInEditMode]
    public class SwatchrAmbientTriLightingColor : MonoBehaviour
    {
        [Header("Warning: This component changes scene settings in Lighting->Scene")]
        public SwatchrColor sky;

        public SwatchrColor equator;
        public SwatchrColor ground;

        private void OnDestroy()
        {
            sky.OnColorChanged -= Apply;
            equator.OnColorChanged -= Apply;
            ground.OnColorChanged -= Apply;
        }

        private void OnDisable()
        {
            sky.OnColorChanged -= Apply;
            equator.OnColorChanged -= Apply;
            ground.OnColorChanged -= Apply;
        }

        private void OnEnable()
        {
            if (sky == null)
            {
                sky = new SwatchrColor();
            }
            if (equator == null)
            {
                equator = new SwatchrColor();
            }
            if (ground == null)
            {
                ground = new SwatchrColor();
            }
            sky.OnColorChanged += Apply;
            sky.OnEnable();

            equator.OnColorChanged += Apply;
            equator.OnEnable();

            ground.OnColorChanged += Apply;
            ground.OnEnable();
        }

        public void Apply()
        {
            if (RenderSettings.ambientMode != UnityEngine.Rendering.AmbientMode.Trilight)
            {
                Debug.LogWarning("[SwatchrAmbientTryLightingColor] RenderSettings.ambientMode != Trilight. Changing the setting to Tri Lighting. Change it manually in Lighting->Scene.");
                RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            }
            RenderSettings.ambientSkyColor = sky.color;
            RenderSettings.ambientEquatorColor = equator.color;
            RenderSettings.ambientGroundColor = ground.color;
        }
    }
}