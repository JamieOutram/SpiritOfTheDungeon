using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimator : MonoBehaviour
{

    Button button = default;

    [SerializeField][Range(-180,180)] float angle = default;
    [SerializeField] float distance = default;
    [SerializeField] float time = default;

    Vector2 finalPosition = default;
    Vector2 initialPosition = default;
    bool isFadingOut = false;
    bool isFadingIn = false;
    float timeElapsed = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingOut)
        {
            isFadingOut = UpdateFade(isFadingOut, FadeOut);
        }
        else if (isFadingIn)
        {
            isFadingIn = UpdateFade(isFadingIn, FadeIn);
            button.interactable = !isFadingIn; //enable button at end of fade in
        }
    }

    public void ResetButton()
    {
        transform.position = initialPosition;
        var tempColor = button.image.color;
        tempColor.a = 1f;
        button.image.color = tempColor;
        button.interactable = true;
    }

    public void TriggerFadeOut()
    {
        if (!isFadingIn)
        {
            initializeForFade();
            isFadingOut = true;
        }
        else
        {
            Invoke("TriggerFadeOut", 1f); //call again after 1 second if currently fading in
        }
    }

    public void TriggerFadeIn()
    {
        if (!isFadingOut)
        {
            initializeForFade();
            isFadingIn = true;
        }
        else
        {
            Invoke("TriggerFadeIn", 0.1f); //call again after 1 second if currently fading out
        }
    }

    private void initializeForFade()
    {
        finalPosition = initialPosition + distance * Vector2.up.Rotate(-angle);
        timeElapsed = 0f;
        button.interactable = false;
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
        transform.position = Vector2.Lerp(initialPosition, finalPosition, timeElapsed / time);
        var tempColor = button.image.color;
        tempColor.a = Mathf.Lerp(1, 0, timeElapsed / time);
        button.image.color = tempColor;
    }

    void FadeIn()
    {
        transform.position = Vector2.Lerp(finalPosition, initialPosition, timeElapsed / time);
        var tempColor = button.image.color;
        tempColor.a = Mathf.Lerp(0, 1, timeElapsed / time);
        button.image.color = tempColor;
    }

    
}
