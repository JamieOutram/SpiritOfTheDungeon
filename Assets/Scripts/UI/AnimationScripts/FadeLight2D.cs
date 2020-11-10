using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FadeLight2D : FadeBase
{
    Light2D light2d = default;
    float initIntensity = 1f;
    protected override void A_OnAwake()
    {
        light2d = GetComponent<Light2D>();
        initIntensity = light2d.intensity;
    }

    protected override void A_FadeIn()
    {
        light2d.intensity = Mathf.Lerp(0, initIntensity, GetTimeRatio());
    }

    protected override void A_ResetInvisible()
    {
        light2d.intensity = 0f;
    }

    protected override void A_FadeOut()
    {
        light2d.intensity = Mathf.Lerp(initIntensity, 0, GetTimeRatio());
    }

    protected override void A_ResetVisible()
    {
        light2d.intensity = initIntensity;
    }

    protected override void A_OnFadeInComplete()
    {
        return;
    }

    protected override void A_OnFadeOutComplete()
    {
        return;
    }
}