using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyInventory : MonoBehaviour
{
    #region Variables
    [Header("Game Items")]
    public Sprite[] ItemSprite;
    public string[] ItemName;

    [Header("Player Inventory")]
    public Image[] SlotImage;
    private bool[] SlotFull;
    private string[] SlotName;
    public Sprite EmptySprite;
    [HideInInspector] public int SlotAvailable;
    [HideInInspector] public bool ItemExists;

    [Header("References")]
    public PlayerMovement PM;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        SlotFull = new bool[SlotImage.Length];
        SlotName = new string[SlotImage.Length];
        SlotAvailable = SlotImage.Length + 1;
    }

    #region Add Item
    public void AddItem(string Name)
    {
        for (int i = 0; i < SlotImage.Length; i++)
        {
            if (SlotFull[i] == false)
            {
                #region Find item's image
                for (int j = 0; j < ItemName.Length; j++)
                { 
                    if (ItemName[j] == Name)
                    {
                        SlotImage[i].sprite = ItemSprite[j];
                        break;
                    }
                }
                #endregion
                //Fill the slot
                SlotFull[i] = true;
                SlotName[i] = Name;
                SlotAvailable--;
                break;
            }
        }
    }
    #endregion

    #region Remove Item
    public void RemoveItem(string Name)
    {
        for (int i = 0; i < SlotImage.Length; i++)
        {
            if (SlotName[i] == Name)
            {
                SlotName[i] = "Empty";
                SlotFull[i] = false;
                SlotImage[i].sprite = EmptySprite;
                SlotAvailable++;
            }
        }
    }
    #endregion

    #region Remove From A Specific Slot
    public void RemoveSpecificItem(int Number)
    {
        SlotName[Number] = "Empty";
        SlotFull[Number] = false;
        SlotImage[Number].sprite = EmptySprite;
        SlotAvailable++;
    }
    #endregion

    #region Use Inventory Item Library
    public void UseItem(int Number)
    {
        switch (SlotName[Number])
        {
            case "Red Potion":
                {
                    RemoveSpecificItem(Number);
                    break;
                }
            case "Blue Potion":
                {
                    RemoveSpecificItem(Number);
                    break;
                }
            case "Apple":
                {
                    RemoveSpecificItem(Number);
                    break;
                }
            case "Scroll":
                {
                    RemoveSpecificItem(Number);
                    break;
                }
        }
    }
    #endregion

    #region Check For Item
    public void CheckForItem(string Name)
    {
        ItemExists = false;
        for (int i = 0; i < SlotName.Length; i++)
        {
            if (SlotName[i] == Name)
            {
                ItemExists = true;
                break;
            }
        }
    }
    #endregion
}
