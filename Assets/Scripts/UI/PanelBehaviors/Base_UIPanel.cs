using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_UIPanel : MonoBehaviour
{
    public bool playAnimations = true;

    [HideInInspector] public bool isOpen = false;
    
    private ButtonBehaviour[] animatedButtons = default;

    public abstract UIPanelId Id { get; }

    public virtual void OpenBehavior()
    {
        if (!isOpen)
        {
            isOpen = true;
            gameObject.SetActive(true);
            if (IsInvoking("TryDeactivate")) CancelInvoke("TryDeactivate"); 
        }

        foreach(ButtonBehaviour button in animatedButtons)
        {
            button.TriggerFadeIn();
        }

    }

    public virtual void AwakeBehavior()
    {
        animatedButtons = GetComponentsInChildren<ButtonBehaviour>();
    }

    public virtual void UpdateBehavior()
    {

    }

    public virtual void CloseBehavior()
    {
        foreach (ButtonBehaviour button in animatedButtons)
        {
            button.TriggerFadeOut();
        }

        if (isOpen)
        {
            isOpen = false;
            Invoke("TryDeactivate", 0.1f);
        }

        
    }
    private void TryDeactivate()
    {
        bool canDeactivate = true;
        foreach(ButtonBehaviour button in animatedButtons)
        {
            if (button.IsFading) canDeactivate = false;
        }
        if (canDeactivate) gameObject.SetActive(false);
        else Invoke("TryDeactivate", 0.1f);

    }

    public virtual void OnCellMouseDown(RoomBehaviour target, Vector2 index)
    {
        Debug.Log("No on mouse down behaviour for this pannel");
    }
}