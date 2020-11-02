using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu_UIPanel : Base_UIPanel
{
    public override UIPanelId Id { get { return UIPanelId.MainMenu; } }
    public Button startButton;
    public Button settings;
    
    public override void OpenBehavior()
    {
        base.OpenBehavior();
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => { StartGameClicked(); });
    }
    void StartGameClicked()
    {
        Debug.Log("Button Pressed");
        UIManager.instance.TriggerPanelTransition(UIManager.instance.FightPanel, true);
    }

    void SettingsClicked()
    {
        Debug.Log("Button Pressed");
        UIManager.instance.TriggerPanelTransition(UIManager.instance.SettingsPanel);
    }
}
