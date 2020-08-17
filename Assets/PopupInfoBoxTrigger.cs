using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupInfoBoxTrigger : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        if (PopupInfoBox.isLoaded)
        {
            UIManager.infoBox.ChangeTarget(gameObject, true);
            UIManager.infoBox.ShowBox();
        }
    }
}
