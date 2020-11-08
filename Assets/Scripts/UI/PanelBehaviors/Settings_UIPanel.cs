using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Settings_UIPanel : Base_UIPanel
{
    public override UIPanelId Id { get { return UIPanelId.Settings; } }
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
        UIManager.instance.TriggerPanelTransition(UIManager.instance.FightPanel);
    }

    void SettingsClicked()
    {
        UIManager.instance.TriggerPanelTransition(UIManager.instance.SettingsPanel);
    }
}
