using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PopupInfoBoxTrigger : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        
        if (!EventSystem.current.IsPointerOverGameObject()) {
            UIManager.infoBox.ShowBox(gameObject, true);
            Debug.Log("showing popup");
        }
        else
        {
            Debug.Log("popup blocked");
        }
    }
}
