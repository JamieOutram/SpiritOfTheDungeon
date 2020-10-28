﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_UIPanel : MonoBehaviour
{
    public bool isOpen = false;

    public abstract UIPanelId Id { get; }

    public virtual void OpenBehavior()
    {
        if (!isOpen)
        {
            isOpen = true;
            gameObject.SetActive(true);
        }
    }

    public virtual void UpdateBehavior()
    {

    }

    public virtual void CloseBehavior()
    {
        if (isOpen)
        {
            isOpen = false;
            gameObject.SetActive(false);
        }
    }

    public virtual void OnCellMouseDown(RoomBehaviour target, Vector2 index)
    {
        Debug.Log("No on mouse down behaviour for this pannel");
    }
}