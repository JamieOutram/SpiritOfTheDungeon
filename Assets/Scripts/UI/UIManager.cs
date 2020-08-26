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
    private Image screenOverlay;
    [SerializeField] private GameObject pauseObject;
    
    public static PopupInfoBox infoBox;

    void Awake()
    {
        PopupInfoBox.LoadResources();
        screenOverlay = GetComponent<Image>();
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

    internal void ShowScroll(bool v1, bool v2)
    {
        throw new NotImplementedException();
    }
}
