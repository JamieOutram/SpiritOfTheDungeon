using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EscMenu_UIPanel : Base_UIPanel
{
    public override UIPanelId Id { get { return UIPanelId.EscMenu; } }
    public override bool IsPopup { get { return true; } }
    public Button backButton;
    

    public override void OpenBehavior()
    {
        base.OpenBehavior();
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(() => { BackClicked(); });
    }
    void BackClicked()
    {
        Debug.Log("Button Pressed");
        UIManager.instance.TriggerPanelTransitionBack();
    }

    void SettingsClicked()
    {
        Debug.Log("Button Pressed");
        UIManager.instance.TriggerPanelTransition(UIManager.instance.SettingsPanel);
    }
}
