using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItem : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    private bool CanInteract;
    public GameObject Message;

    [Header("References")]
    public InputManager IM;
    #endregion

    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.tag  == "Player")
        {
            CanInteract = true;
            Message.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D Object)
    {
        if (Object.tag == "Player" && CanInteract == true && Input.GetKeyDown(IM.Interaction))
            Debug.Log("Interaction");
    }

    private void OnTriggerExit2D(Collider2D Object)
    {
        if (Object.tag == "Player")
        {
            CanInteract = false;
            Message.SetActive(false);
        }
    }
}
