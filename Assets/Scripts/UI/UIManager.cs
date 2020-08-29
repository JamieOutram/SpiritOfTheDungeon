using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    [HideInInspector] public GameObject targetObj;
    //public Transform parent;
    [SerializeField] private GameObject pauseObject;
    [SerializeField] private GameObject leftScrollObj;
    [SerializeField] private GameObject rightScrollObj;
    public static PopupInfoBox infoBox;
    public float speedUpSpeed;

    void Awake()
    {
        PopupInfoBox.LoadResources();
    }
    // Start is called before the first frame update
    void Start()
    {
        infoBox = new PopupInfoBox(targetObj, gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        PauseControl.PauseGame(true);
        pauseObject.SetActive(true);
    }

    public void ResumeGame()
    {
        PauseControl.PauseGame(false);
        pauseObject.SetActive(false);
    }

    internal void ShowScroll(bool left, bool right)
    {
        rightScrollObj.SetActive(right);
        leftScrollObj.SetActive(left);
    }

    public void SpeedUp()
    {
        PauseControl.SetGameSpeed(speedUpSpeed);
    }
}
