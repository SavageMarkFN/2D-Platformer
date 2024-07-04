using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    //Privates
    private int FoundSlot;
    private bool InventoryOpened;
    private string FoundRemoveItemName;

    //Publics
    [Header("Sprite Variables")]
    public Sprite[] ItemSprites;
    public string[] ItemNames;
    [Header("Inventory Variables")]
    public Image[] SlotImage;
    public string[] SlotName;
    public bool[] SlotFull;
    public int SlotAvailable;

    //Scripts
    public PlayerMovement playerMovement;

    //Animator
    private Animator ThisAnimator;

    private void Start()
    {
        ThisAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryOpened == false)
            {
                InventoryOpened = true;
                playerMovement.PlayerFreeze = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ThisAnimator.SetTrigger("Open");
                playerMovement.animController.WarriorAnimator.SetFloat("Speed", 0);
            }
            else
            {
                InventoryOpened = false;
                playerMovement.PlayerFreeze = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                ThisAnimator.SetTrigger("Open");
            }
        }    
    }

    #region Set and Remove Item
    public void SetUpItem(string ItemName)
    {
        if (SlotAvailable > 0)
        {
           //Stage 1 : Find an open slot
           for (int i = 0; i < SlotFull.Length; i++)
           {
                if (SlotFull[i] == false)
                {
                    SlotFull[i] = true;
                    SlotName[i] = ItemName;
                    SlotAvailable -= 1;
                    FoundSlot = i;
                    Debug.Log(ItemName + SlotAvailable);
                    break;
                }
           }

            //Stage 2 : Find the sprite and add it
            for (int i = 0; i < ItemSprites.Length; i++)
            {
                if (ItemName == ItemNames[i])
                {
                    Debug.Log("Found a sprite");
                    SlotImage[FoundSlot].sprite = ItemSprites[i];
                    break;
                }
            }
        }
        else
        {
            Debug.Log("InventoryFull");
        }    
    }

    public void RemoveItem(int Number, string ItemName)
    {
        for (int i = 0; i < SlotFull.Length; i++)
        {
            if (SlotName[i] == ItemName && i == Number)
            {
                SlotFull[i] = false;
                SlotName[i] = null;
                SlotAvailable += 1;
                SlotImage[i].sprite = ItemSprites[0];
                Debug.Log(ItemName + "Removed from inventory");
                break;
            }
        }
    }
    #endregion

    public void Button(int Number)
    {
       if (SlotFull[Number] == true)
       {
           if (SlotName[Number] == ItemNames[1] && playerMovement.Health < 100)
           {
                FoundRemoveItemName = ItemNames[1];
                playerMovement.Regend(25, true);
                Debug.Log("Heal 25");
           }
           else if (SlotName[Number] == ItemNames[2] && playerMovement.Health < 100)
           {
                FoundRemoveItemName = ItemNames[2];
                playerMovement.Regend(50, true);
                Debug.Log("Heal 50");
           }
           else if (SlotName[Number] == ItemNames[3] && playerMovement.Health < 100)
           {
                FoundRemoveItemName = ItemNames[3];
                playerMovement.Regend(100, true);
                Debug.Log("Heal 100");
           }
           else if (SlotName[Number] == ItemNames[4] && playerMovement.Mana < 100)
           {
                FoundRemoveItemName = ItemNames[4];
                playerMovement.Regend(25, false);
                Debug.Log("Mana 25");
           }
           else if (SlotName[Number] == ItemNames[5] && playerMovement.Mana < 100)
           {
                FoundRemoveItemName = ItemNames[5];
                playerMovement.Regend(50, false);
                Debug.Log("Mana 50");
           }
           else if (SlotName[Number] == ItemNames[6] && playerMovement.Mana < 100)
           {
                FoundRemoveItemName = ItemNames[6];
                playerMovement.Regend(100, false);
                Debug.Log("Mana 100");
           }
           RemoveItem(Number,FoundRemoveItemName);
        }
    }
}
