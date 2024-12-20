using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItem : MonoBehaviour
{
    #region Variables
    private bool InRange;

    [Header("References")]
    public MyInventory inventory;

    [Header("Interaction")]
    public GameObject Message;
    public string Item;
    #endregion

    #region On Triggers
    private void OnTriggerStay2D(Collider2D Object)
    {
        if (Object.name == "Player")
        {
            InRange = true;
            Message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D Object)
    {
        if (Object.name == "Player")
        {
            InRange = false;
            Message.SetActive(false);
        }
    }
    #endregion

    #region Start and Update
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && InRange == true)
        {
            InRange = false;
            Message.SetActive(false);
            inventory.AddItem(Item);
            this.gameObject.SetActive(false);
        }
    }
    #endregion
}
