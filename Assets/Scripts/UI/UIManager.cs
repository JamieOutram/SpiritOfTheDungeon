using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameObject targetObj;
    //public Transform parent;
    private Image screenOverlay;
    
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
    }

    public void ResumeGame()
    {
        PauseControl.PauseGame(false);
    }

}
