using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainMenu_UIPanel : Base_UIPanel
{
    public Button startButton;
    public override void OpenBehavior()
    {
        base.OpenBehavior();
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => { StartTrainingPressed(); });
    }

    void StartTrainingPressed()
    {
        UIManager.instance.TriggerPanelTransition(UIManager.instance.TrainingPanel);
    }
}
