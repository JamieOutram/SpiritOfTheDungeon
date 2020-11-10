using System;
using UnityEngine;

public abstract class FadeBase : MonoBehaviour
{
    //Actions on awake
    protected abstract void A_OnAwake();
    //Actions after fading in are complete
    protected abstract void A_OnFadeInComplete();
    //Actions after fading out are complete
    protected abstract void A_OnFadeOutComplete();
    //Actions to reset for fading out (Visible state)
    protected abstract void A_ResetVisible();
    //Actions to reset for fading in (Invisible state)
    protected abstract void A_ResetInvisible();
    //Action carried out every frame during fade out
    protected abstract void A_FadeOut();
    //Action carried out every frame during fade in
    protected abstract void A_FadeIn();

    [SerializeField] [Range(-180, 180)] float angle = default;
    [SerializeField] float distance = default;
    [SerializeField] float time = 0.1f;

    Vector2 FinalPosition 
    {
        get { return initialPosition + distance * Vector2.up.Rotate(-angle); }
    }
    Vector2 initialPosition = default;
    bool isFadingOut = false;
    public bool IsFading
    {
        get { return isFadingOut || isFadingIn; }
    }
    bool isFadingIn = false;
    float timeElapsed = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        A_OnAwake();
        if (time <= 0f) time = 0.1f;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingOut)
        {
            isFadingOut = UpdateFade(isFadingOut, FadeOut);
            if (!IsFading) A_OnFadeOutComplete();
        }
        else if (isFadingIn)
        {
            isFadingIn = UpdateFade(isFadingIn, FadeIn);
            if (!IsFading) A_OnFadeInComplete();
        }
    }

    public void Reset(bool isFaded = false)
    {
        isFadingIn = false;
        isFadingOut = false;
        timeElapsed = 0f;

        if (!isFaded)
        {
            transform.position = initialPosition;
            A_ResetVisible();
        }
        else
        {
            transform.position = FinalPosition;
            A_ResetInvisible();
        }
    }

    public void TriggerFadeOut()
    {
        Reset(false);
        isFadingOut = true;
    }

    public void TriggerFadeIn()
    {
        Reset(true);
        isFadingIn = true;
    }

    private bool UpdateFade(bool isFading, Action updateMethod)
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= time)
            timeElapsed = time;

        updateMethod();

        if (timeElapsed == time)
        {
            isFading = false;
        }

        return isFading;
    }

    void FadeOut()
    {
        transform.position = Vector2.Lerp(initialPosition, FinalPosition, GetTimeRatio());
        A_FadeOut();
        //var tempColor = image.color;
        //tempColor.a = Mathf.Lerp(1, 0, timeElapsed / time);
        //image.color = tempColor;
    }

    void FadeIn()
    {
        transform.position = Vector2.Lerp(FinalPosition, initialPosition, GetTimeRatio());
        A_FadeIn();
        //var tempColor = image.color;
        //tempColor.a = Mathf.Lerp(0, 1, timeElapsed / time);
        //image.color = tempColor;
    }

    protected float GetTimeRatio()
    {
        return timeElapsed / time;
    }
}
