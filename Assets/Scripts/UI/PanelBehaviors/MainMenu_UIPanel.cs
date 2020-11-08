using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu_UIPanel : Base_UIPanel
{
    public override UIPanelId Id { get { return UIPanelId.MainMenu; } }
    public override bool IsPopup { get { return false; } }
    public Button startButton;
    public Button tempButton;
    public Button settings;
    
    public override void OpenBehavior()
    {
        base.OpenBehavior();
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => { StartGameClicked(); });
        tempButton.onClick.RemoveAllListeners();
        tempButton.onClick.AddListener(() => { TempClicked(); });
    }
    void StartGameClicked()
    {
        UIManager.instance.TriggerPanelTransition(UIManager.instance.FightPanel);
        //UIManager.instance.TriggerPanelTransition(UIManager.instance.FightPanel,true);
    }

    void SettingsClicked()
    {
        UIManager.instance.TriggerPanelTransition(UIManager.instance.SettingsPanel);
    }

    //DEBUG
    void TempClicked()
    {
        Debug.Log("Button Pressed");
        UIManager.instance.TriggerPanelTransition(UIManager.instance.EscMenuPanel);
    }
}
