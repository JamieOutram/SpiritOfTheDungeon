using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSprite : FadeBase
{
    SpriteRenderer sprite = default;
    protected override void A_FadeIn()
    {
        SetAlpha(Mathf.Lerp(0, 1, GetTimeRatio()));
    }

    protected override void A_FadeOut()
    {
        SetAlpha(Mathf.Lerp(1, 0, GetTimeRatio()));
    }

    protected override void A_OnAwake()
    {
        sprite = GetComponent<SpriteRenderer>();
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
        SetAlpha(0f);
    }

    protected override void A_ResetVisible()
    {
        SetAlpha(1f);
    }

    private void SetAlpha(float alpha)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }
}
