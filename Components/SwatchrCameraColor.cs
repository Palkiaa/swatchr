using UnityEngine;

namespace swatchr.components
{
    [RequireComponent(typeof(Camera))]
    public class SwatchrCameraColor : SwatchrColorApplier
    {
        [HideInInspector]
        public Camera swatchingCamera;

        public override void Apply()
        {
            if (swatchingCamera == null)
            {
                swatchingCamera = GetComponent<Camera>();
            }
            swatchingCamera.backgroundColor = swatchrColor.color;
        }
    }
}