using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyInventory : MonoBehaviour
{
    #region Variables
    [Header("Game Items")]
    public Sprite[] ItemSprite;
    public string[] ItemName;

    [Header("Player Inventory")]
    public Image[] SlotImage;
    public bool[] SlotFull;
    public string[] SlotName;
    public Sprite EmptySprite;
    [HideInInspector] public int SlotAvailable;
    [HideInInspector] public bool ItemExists;

    [Header("Extras")]
    public TextMeshProUGUI[] TextAmount; //0 Health Text, 1 Mana Text
    public int HPAmount;
    public int MPAmount;

    [Header("References")]
    public PlayerMovement PM;
    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        #region Potion Handler
        
        #endregion
    }

    #region Add Item
    public void AddItem(string Name)
    {
        for (int i = 0; i < SlotImage.Length; i++)
        {
            if (SlotFull[i] == false)
            {
                SlotFull[i] = true;
                SlotName[i] = Name;
                SlotAvailable--;
                #region Find Item's Image
                for (int j = 0; j < ItemName.Length; i++)
                {
                    if (ItemName[j] == Name)
                    {
                        SlotImage[i].sprite = ItemSprite[j];
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
                SlotImage[i].sprite = EmptySprite;
                SlotFull[i] = false;
                SlotName[i] = "Empty";
                SlotAvailable++;
                break;
            }
        }
    }
    #endregion

    #region Check For Item
    public void CheckForItem(string Name)
    {
        ItemExists = false;
        for (int i = 0; i < SlotImage.Length; i++)
        {
            if (SlotName[i] == Name)
            {
                ItemExists = true;
                break;
            }
        }
    }
    #endregion

    #region Use Inventory Item Library
    public void UseItem(int Number)
    {
        if (SlotName[Number] == "Health Potion")
        {
            PM.Health += 100;
            if (PM.Health > PM.MaxHealth)
                PM.Health = PM.MaxHealth;
            //Remove the item from the slot
            SlotImage[Number].sprite = EmptySprite;
            SlotFull[Number] = false;
            SlotName[Number] = "Empty";
            SlotAvailable++;
        }
    }
    #endregion
}
