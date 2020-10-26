using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//All Button Behaviour triggers and camera controls for the training interface
public class Base_InfoBox_UIPanel : Base_UIPanel
{

    public PopupInfoBox infoBox;

    public override void OpenBehavior()
    {
        base.OpenBehavior();
        PopupInfoBox.LoadResources();
        infoBox = new PopupInfoBox(transform);
    }

    public override void CloseBehavior()
    {
        base.CloseBehavior();
        infoBox.DestroyBox();
    }

    public override void OnCellMouseDown(RoomBehaviour target, Vector2 index)
    {
        if (!ReferenceEquals(infoBox, null))
            infoBox.HideBox();
    }
}
