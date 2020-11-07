using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Base_UIPanel : MonoBehaviour
{
    public bool playAnimations = true;

    [HideInInspector] public bool isOpen = false;
    
    protected ButtonBehaviour[] animatedButtons = default;

    protected Button[] allButtons = default;

    public abstract UIPanelId Id { get; }
    public abstract bool IsPopup { get; }

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
        allButtons = GetComponentsInChildren<Button>();
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

    public void SetPanelInput(bool enable)
    {
        foreach(Button button in allButtons)
        {
            button.interactable = enable;
        }
    }

}