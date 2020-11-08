using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//All Button Behaviour triggers and camera controls for the training interface
public class Fight_UIPanel : Base_InfoBox_UIPanel
{
    public override UIPanelId Id { get { return UIPanelId.Fight; } }
    public override bool IsPopup { get { return false; } }
    [SerializeField] private Button pause = default;
    [SerializeField] private Text pauseText = default;
    [SerializeField] private Button play = default;
    [SerializeField] private Button back = default;
    [SerializeField] private Button speedUp = default;

    private CycleImage speedUpCycler;


    void Awake()
    {
        speedUpCycler = speedUp.transform.GetComponent<CycleImage>();
    }

    // Start is called before the first frame update
    public override void OpenBehavior()
    {
        base.OpenBehavior();
    }

    public override void CloseBehavior()
    {
        base.CloseBehavior();
    }

    public void PauseGame()
    {
        TimeControl.PauseGame(true);
        pauseText.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        TimeControl.PauseGame(false);
        pauseText.gameObject.SetActive(false);
    }

    public void SpeedUpTrigger()
    {
        TimeControl.NextSpeed();
        speedUpCycler.Set(TimeControl.Index);
    }

    public void BackTrigger()
    {
        ResumeGame();
        TimeControl.GameSpeed = 1f;
        speedUpCycler.Set(TimeControl.Index);
        UIManager.instance.TriggerPanelTransition(UIManager.instance.MainMenuPanel);
    }


}
