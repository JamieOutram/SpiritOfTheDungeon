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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        infoBox.HideBox();
        //Debug.Log("OnMouseDown called");
    }
}
