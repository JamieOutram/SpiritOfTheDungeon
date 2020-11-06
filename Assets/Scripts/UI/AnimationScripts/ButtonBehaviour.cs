using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{

    Button button = default;
    Image image = default;

    [SerializeField] [Range(-180, 180)] float angle = default;
    [SerializeField] float distance = default;
    [SerializeField] float time = 0.1f;

    Vector2 finalPosition = default;
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

        button = GetComponent<Button>();
        if (button != null) image = button.image;
        else image = GetComponent<Image>();

        if (time == 0f) time = 0.1f;
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
            if (button != null) button.interactable = !isFadingIn; //enable button at end of fade in
        }
    }

    public void ResetButton(bool isFaded = false)
    {
        isFadingIn = false;
        isFadingOut = false;

        var tempColor = image.color;
        if (!isFaded)
        {
            transform.position = initialPosition;
            tempColor.a = 1f;
            if (button != null) button.interactable = true;
        }
        else
        {
            initializeForFade();
            transform.position = finalPosition;
            tempColor.a = 0f;
            if (button != null) button.interactable = false;
        }
        image.color = tempColor;
    }

    public void TriggerFadeOut()
    {
        ResetButton(false);
        initializeForFade();
        isFadingOut = true;
    }

    public void TriggerFadeIn()
    {
        ResetButton(true);
        initializeForFade();
        isFadingIn = true;
    }

    private void initializeForFade()
    {
        finalPosition = initialPosition + distance * Vector2.up.Rotate(-angle);
        timeElapsed = 0f;
        if (button != null) button.interactable = false;
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
        var tempColor = image.color;
        tempColor.a = Mathf.Lerp(1, 0, timeElapsed / time);
        image.color = tempColor;
    }

    void FadeIn()
    {
        transform.position = Vector2.Lerp(finalPosition, initialPosition, timeElapsed / time);
        var tempColor = image.color;
        tempColor.a = Mathf.Lerp(0, 1, timeElapsed / time);
        image.color = tempColor;
    }


}
