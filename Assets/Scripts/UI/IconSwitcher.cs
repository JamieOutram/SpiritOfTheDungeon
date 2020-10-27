using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSwitcher : MonoBehaviour
{
    public Image iconImage;

    public void SwitchIcon(Sprite newIcon)
    {
        //Debug.Log(iconImage);
        iconImage.sprite = newIcon;
    }
}
