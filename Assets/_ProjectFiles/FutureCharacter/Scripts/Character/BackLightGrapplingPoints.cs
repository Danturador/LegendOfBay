using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BackLightGrapplingPoints : MonoBehaviour
{
    [SerializeField] private Transform[] grapplingPoints;
    [SerializeField] private ParticleSystem grapplingBackLight;
    [SerializeField] private List<ParticleSystem> activeParticleEffect;
    [SerializeField] private bool activateBacklight;

    public void BackLightActivate()
    {
        if (activateBacklight)
        {
            if (activeParticleEffect.Count >= 1)
            {
                foreach (ParticleSystem particleEffect in activeParticleEffect)
                {
                    particleEffect.Play();
                }
            }
            else
            {
                foreach (Transform point in grapplingPoints)
                {
                    ParticleSystem particleEffect = Instantiate(grapplingBackLight, point.position, Quaternion.identity);
                    activeParticleEffect.Add(particleEffect);
                    particleEffect.Play();
                }
            }
        }
       
    }

    public void BackLightDisable()
    {
       if (activeParticleEffect.Count >= 1)
        {
            foreach (ParticleSystem particleEffect in activeParticleEffect)
            {
                particleEffect.Stop();
            }
        }
    }

    public void EnableBacklight()
    {
        activateBacklight = true;
    }

    public bool BackLightEnabled()
    {
        return activateBacklight;
    }
}
