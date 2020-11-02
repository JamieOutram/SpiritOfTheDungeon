using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemTrigger : MonoBehaviour
{
    public ParticleSystem ps;

    public void StartParticleSystem()
    {
        if (!ps.IsAlive())
        {
            ps.Play();
        }
    }

    public void StopParticleSystem()
    {
        if (ps.isPlaying)
        {
            ps.Stop();
        }
    }
}
