using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//All Button Behaviour triggers and camera controls for the training interface
public class Fight_UIPanel : Base_UIPanel
{

    public PopupInfoBox infoBox;
    [SerializeField] private Button pause;
    [SerializeField] private Text pauseText;
    [SerializeField] private Button play;
    [SerializeField] private Button back;
    [SerializeField] private Button speedUp;

    private CycleImage speedUpCycler;

    void Awake()
    {
        PopupInfoBox.LoadResources();
        speedUpCycler = speedUp.transform.GetComponent<CycleImage>();
    }

    // Start is called before the first frame update
    public override void OpenBehavior()
    {
        base.OpenBehavior();
        infoBox = new PopupInfoBox(transform);
    }

    public override void CloseBehavior()
    {
        base.CloseBehavior();
        infoBox.DestroyBox();
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
        
    }

    public override void OnCellMouseDown(RoomBehaviour target, Vector2 index)
    {
        if (!ReferenceEquals(infoBox, null))
            infoBox.HideBox();
    }
}
