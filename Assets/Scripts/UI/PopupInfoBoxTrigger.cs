using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PopupInfoBoxTrigger : CustomBehaviour
{
    public override void MyOnMouseDown()
    {
        return;
    }
    public override void MyOnMouseUp()
    {
        
        if (!EventSystem.current.IsPointerOverGameObject()) {
            if(!ReferenceEquals(UIManager.instance.TrainingPanel.infoBox,null))
                UIManager.instance.TrainingPanel.infoBox.ShowBox(gameObject, true);
            if (!ReferenceEquals(UIManager.instance.FightPanel.infoBox, null))
                UIManager.instance.FightPanel.infoBox.ShowBox(gameObject, true);
            Debug.Log("showing popup");
        }
        else
        {
            Debug.Log("popup blocked");
        }
    }
}
