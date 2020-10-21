using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//All Button Behaviour triggers and camera controls for the training interface
public class Training_UIPanel : Base_UIPanel
{
    
    public PopupInfoBox infoBox;

    RoomScroller scroller;

    [SerializeField] private Button scrollLeft;
    [SerializeField] private Button scrollRight;
    [SerializeField] private Button pause;
    [SerializeField] private Text pauseText;
    [SerializeField] private Button play;
    [SerializeField] private Button back;
    [SerializeField] private Button speedUp;

    void Awake()
    {
        PopupInfoBox.LoadResources();
    }

    // Start is called before the first frame update
    public override void OpenBehavior()
    {
        base.OpenBehavior();
        infoBox = new PopupInfoBox(transform);
        scroller = new RoomScroller();
        
    }

    public override void CloseBehavior()
    {
        base.CloseBehavior();
        infoBox.DestroyBox();
    }

    public void ScrollRightTrigger()
    {
        if (!CameraController.isCameraZooming)
            scroller.ScrollRight();
    }

    public void ScrollLeftTrigger()
    {
        if (!CameraController.isCameraZooming)
            scroller.ScrollLeft();
    }

    public void ShowScroll(bool left, bool right)
    {
        scrollLeft.gameObject.SetActive(left);
        scrollRight.gameObject.SetActive(right);
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
        TimeControl.ToggleSpeedUp();
    }

    public void BackTrigger()
    {
        if (GridManager.instance.selectedObj != null)
        {
            if (!CameraController.isCameraZooming)
            {
                //If a room is selected and the camera is not currently zooming 
                //perform zoom out and deselection
                scroller.Update(1.5f);
                GridManager.instance.DeselectCell();
            }
        }
        else
        {
            //if nothing is currently selected go back to menu
            UIManager.instance.TriggerPanelTransition(UIManager.instance.TrainMenuPanel);
        }
    }

    public void OnCellMouseDown(RoomBehaviour target, Vector2 index)
    {
        if(!ReferenceEquals(infoBox,null))
            infoBox.HideBox();

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            
            if (!CameraController.isCameraZooming && target.isSelectable)
            {
                float size = target.GetComponent<Collider2D>().bounds.size.y / 2;
                CameraController.ZoomCameraWithRampUpDown(target.transform.position, size, 1.5f, 0.5f);
                GridManager.instance.SelectCell(target, index);
            }
        }
        else
        {
            Debug.Log("Cell Selection blocked");
        }
    }
}
