using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyMerchant : MonoBehaviour
{
    #region Variables
    [Header("Interaction")]
    public GameObject Message;
    private bool InRange;

    [Header("References")]
    public UnityEvent OpenShopEvent;
    public UnityEvent CloseShopEvent;
    #endregion

    #region On Trigger
    void OnTriggerStay2D(Collider2D Object)
    {
        if (Object.name == "Player")
        {
            InRange = true;
            Message.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D Object)
    {
        if (Object.name == "Player")
        {
            InRange = false;
            Message.SetActive(false);
        }
    }
    #endregion

    void Update()
    {
        if (InRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
                OpenShopEvent.Invoke();

            if (Input.GetKeyDown(KeyCode.Escape))
                CloseShopEvent.Invoke();

            if (Input.GetKeyDown(KeyCode.E) && Message.activeSelf == false)
                CloseShopEvent.Invoke();
        }   
    }
}
