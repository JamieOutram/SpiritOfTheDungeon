using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject targetObj;
    //public Transform parent;


    private PopupInfoBox infoBox;

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
}
