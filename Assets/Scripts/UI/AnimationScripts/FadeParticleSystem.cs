using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeParticleSystem : FadeBase
{
    ParticleSystem ps = default;
    protected override void A_FadeIn()
    {
        ps.Play(true);
    }

    protected override void A_FadeOut()
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    protected override void A_OnAwake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    protected override void A_OnFadeInComplete()
    {
        return;
    }

    protected override void A_OnFadeOutComplete()
    {
        return;
    }

    protected override void A_ResetInvisible()
    {
        ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    protected override void A_ResetVisible()
    {
        ps.Play(true);
    }
}
