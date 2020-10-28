using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//All Button Behaviour triggers and camera controls for the training interface
public class Training_UIPanel : Base_InfoBox_UIPanel
{
    public override UIPanelId Id { get { return UIPanelId.TrainMenu; } }
    RoomScroller scroller;

    [SerializeField] private Button scrollLeft = default;
    [SerializeField] private Button scrollRight = default;
    [SerializeField] private Button pause = default;
    [SerializeField] private Text pauseText = default;
    [SerializeField] private Button play = default;
    [SerializeField] private Button back = default;
    [SerializeField] private Button speedUp = default;

    // Start is called before the first frame update
    public override void OpenBehavior()
    {
        base.OpenBehavior();
        scroller = new RoomScroller();
        
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

    public override void OnCellMouseDown(RoomBehaviour target, Vector2 index)
    {
        base.OnCellMouseDown(target, index);
  
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
