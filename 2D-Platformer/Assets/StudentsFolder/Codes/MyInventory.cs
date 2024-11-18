using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyInventory : MonoBehaviour
{
    #region Variables
    [Header("Game Items")]
    public Sprite[] ItemImage;
    public string[] ItemName;
    public Sprite EmptySprite;

    [Header("Player Inventory")]
    public Image[] SlotImage;
    [HideInInspector] public string[] SlotName;
    [HideInInspector] public bool[] SlotFull;
    [HideInInspector] public int SlotAvailable;
    [HideInInspector] public bool Check;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        SlotName = new string[SlotImage.Length];
        SlotFull = new bool[SlotImage.Length];
        SlotAvailable = SlotImage.Length + 1;
    }

    #region Add Item
    public void AddItem(string Name)
    {
        //Find the empty slot
        for (int i = 0; i < SlotImage.Length; i++)
        {
            if (SlotFull[i] == false)
            {
                #region Search for item image
                for (int j = 0; j < ItemName.Length; j++)
                {
                    if (ItemName[j] == Name)
                    {
                        SlotFull[i] = true;
                        SlotName[i] = Name;
                        SlotImage[i].sprite = ItemImage[j];
                        SlotAvailable -= 1;
                        break;
                    }
                }
                #endregion
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
                SlotFull[i] = false;
                SlotName[i] = "Empty";
                SlotImage[i].sprite = EmptySprite;
                SlotAvailable += 1;
                break;
            }
        }
    }
    #endregion

    #region Check for item
    public void CheckForItem(string Name)
    {
        Check = false;
        for (int i = 0; i < SlotImage.Length; i++)
        {
            if (SlotName[i] == Name)
            {
                Check = true;
                break;
            }
        }
    }
    #endregion 
}
