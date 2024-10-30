using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestExample : MonoBehaviour
{
    /*
    Follow Up

    1)OnTrigger Collision
    (One message will appear when the player is close)
    and it will disappear when the player leaves the chest)

    2)Interaction with update method
    When the player press the E button

    3)Adding the Items into the Player (Check Player's Inspector)

    4)Adding Item into the player's inventory
    (Run Inventory function)
    */

    #region Interaction
    [Header("Interaction")]
    private bool CanInteract;
    public GameObject Message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Message.SetActive(true);
            CanInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Message.SetActive(false);
            CanInteract = false;
        }
    }
    #endregion

    private bool Opened;
    public string[] Item;

    [Header("References")]
    public InventoryController IC;
    public Animator ChestAnimator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && CanInteract == true && Opened == false)
        {
            Message.SetActive(false);
            CanInteract = false;
            ChestAnimator.SetTrigger("Open");
            for (int i = 0; i < Item.Length; i++)
            {
                IC.AddItem(Item[i]);
            }
            Opened = true;
        }
    }
}
