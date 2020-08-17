using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupInfoBoxTrigger : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        UIManager.infoBox.ShowBox(gameObject, true);
    }
}
