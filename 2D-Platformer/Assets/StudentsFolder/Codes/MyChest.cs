using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyChest : MonoBehaviour
{
    private bool InRange;
    public GameObject Message;
    public string Item;
    private InventoryController IC;
    private Animator animator;

    private void OnTriggerEnter2D(Collider2D Object)
    {
      if(Object.tag == "Player")
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
        IC = GameObject.Find("/MaxPrefab/Player").GetComponent<InventoryController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.E) && InRange == true)
      {
          InRange = false;
          Message.SetActive(false);
          IC.AddItem(Item);
          animator.SetTrigger("Open");
          this.enabled = false;
      }       
    }
}
