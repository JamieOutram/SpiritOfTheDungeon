using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainMenu_UIPanel : Base_UIPanel
{
    public override UIPanelId Id { get { return UIPanelId.TrainMenu; } }
    public override bool IsPopup { get { return false; } }
    public Button startButton;
    public override void OpenBehavior()
    {
        base.OpenBehavior();
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => { StartTrainingPressed(); });
    }

    void StartTrainingPressed()
    {
        Debug.Log("Button Pressed");
        UIManager.instance.TriggerPanelTransition(UIManager.instance.TrainingPanel);
    }
}
