using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyChest : MonoBehaviour
{
    private bool InRange;
    private InventoryController IC;

    public GameObject Message;
    public string Item;

    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.tag == "Player")
        {
            InRange = true;
            Message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D Object)
    {
        if (Object.tag == "Player")
        {
            InRange = false;
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
      if(Input.GetKeyDown(KeyCode.E) && InRange == true)
      {
          InRange = false;
          Message.SetActive(false);
          IC.AddItem(Item);
          this.gameObject.SetActive(false);
      }       
    }
}
