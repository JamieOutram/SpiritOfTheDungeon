using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IPointerDownHandler
{
    
    public GameObject targetObj;
    //public Transform parent;

    
    public static PopupInfoBox infoBox;

    void Awake()
    {
        PopupInfoBox.LoadResources();
    }
    // Start is called before the first frame update
    void Start()
    {
        infoBox = new PopupInfoBox(targetObj, gameObject.transform);
        Debug.Log(infoBox);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Displays Popup with passed object info
    public static void ShowUnitInfoPopup(GameObject target)
    {
        if (PopupInfoBox.isLoaded)
        {
            infoBox.ChangeTarget(target, true);
            infoBox.ShowBox();
        }
    }
    private void OnMouseUpAsButton()
    {
        ShowUnitInfoPopup(targetObj);
        Debug.Log("OnMouseUpAsButton called");
    }

    //Hides the onclick popup
    public static void HideUnitInfoPopup()
    {
        if (PopupInfoBox.isLoaded)
        {
            infoBox.HideBox();
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HideUnitInfoPopup();
        //Debug.Log("OnMouseDown called");
    }
}
