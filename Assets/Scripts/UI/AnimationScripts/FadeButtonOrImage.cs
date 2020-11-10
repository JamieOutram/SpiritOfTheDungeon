using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.UI;

public class FadeButtonOrImage : FadeBase
{
    Button button = default;
    Image image = default;

    protected override void A_OnAwake()
    {
        button = GetComponent<Button>();
        if (button != null) image = button.image;
        else image = GetComponent<Image>();
    }

    protected override void A_OnFadeInComplete()
    {
        if (button != null) button.interactable = true;
    }

    protected override void A_OnFadeOutComplete()
    {
        return;
    }

    protected override void A_ResetVisible()
    {
        if (button != null) button.interactable = true;
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
    }

    protected override void A_ResetInvisible()
    {
        if (button != null) button.interactable = false;
        var tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
    }

    protected override void A_FadeOut()
    {
        var tempColor = image.color;
        tempColor.a = Mathf.Lerp(1, 0, GetTimeRatio());
        image.color = tempColor;
    }

    protected override void A_FadeIn()
    {
        var tempColor = image.color;
        tempColor.a = Mathf.Lerp(0, 1, GetTimeRatio());
        image.color = tempColor;
    }
}
