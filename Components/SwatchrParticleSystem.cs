using UnityEngine;

namespace swatchr.components
{
    [RequireComponent(typeof(ParticleSystem))]
    public class SwatchrParticleSystem : SwatchrColorApplier
    {
        private ParticleSystem swatchingParticleSystem;

        public override void Apply()
        {
            if (swatchingParticleSystem == null)
            {
                swatchingParticleSystem = GetComponent<ParticleSystem>();
            }
            var main = swatchingParticleSystem.main;
            main.startColor = swatchrColor.color;
        }
    }
}