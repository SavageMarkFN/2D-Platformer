using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyMerchant : MonoBehaviour
{
    //One Background 1920 x 1080 (192 x 108) (Shop_Background)
    //One Frame 1920 x 1080 (192 x 108) (Shop_Frame)
    //One Background 300 x 350 (30 x 35) (ShopItem_Background)
    //One Frame 300 x 350 (30 x 35) (ShopItem_Frame)
    //One Button 200 x 60 (20x6) (ShopBuy_Button)

    private bool CanInteract;
    public GameObject ThisCanvas;
    public GameObject Message;
    public UnityEvent OpenShopEvent;
    public UnityEvent CloseShopEvent;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player" && !ThisCanvas.activeSelf)
        {
            CanInteract = true;
            Message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            CanInteract = false;
            Message.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanInteract)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ThisCanvas.SetActive(true);
            Message.SetActive(false);
            CanInteract = false;
            OpenShopEvent.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && ThisCanvas.activeSelf) 
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ThisCanvas.SetActive(false);
            CloseShopEvent.Invoke();
        }
    }
}
