using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyChest : MonoBehaviour
{
    private bool InRange;
    private MyInventory inventory;
    private Animator animator;
    private bool Opened;

    public GameObject Message;
    public string[] Item;

    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.tag == "Player" && Opened == false)
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
        inventory = GameObject.Find("/MaxPrefab/Player").GetComponent<MyInventory>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.E) && InRange == true)
      {
          InRange = false;
          Message.SetActive(false);
          animator.SetTrigger("Open");
          for (int i = 0; i < Item.Length; i++)
                inventory.AddItem(Item[i]);
          Opened = true;
      }       
    }
}
