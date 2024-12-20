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
    public int HPAmount;
    public int MPAmount;
    public TextMeshProUGUI[] TextAmount; //0 Health Text, 1 Mana Text

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
        if (Input.GetKeyDown(KeyCode.Alpha1) && HPAmount > 0)
        {
            PM.Health += 50;
            if (PM.Health > PM.MaxHealth)
                PM.Health = PM.MaxHealth;
            HPAmount--;
            RemoveItem("Health Potion");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && MPAmount > 0)
        {
            PM.Mana += 50;
            if (PM.Mana > PM.MaxMana)
                PM.Mana = PM.MaxMana;
            MPAmount--;
            RemoveItem("Mana Potion");
        }

        TextAmount[0].text = HPAmount.ToString();
        TextAmount[1].text = MPAmount.ToString();
        #endregion
    }

    #region Add Item
    public void AddItem(string Name)
    {
        for (int i = 0; i < SlotName.Length; i++)
        {
            if (SlotFull[i] == false)
            {
                SlotFull[i] = true;
                SlotName[i] = Name;
                SlotAvailable--;
                #region Find the item's image
                for (int j = 0; j < ItemName.Length; j++)
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

        if (Name == "Health Potion")
            HPAmount++;
        else if (Name == "Mana Potion")
            MPAmount++;
    }
    #endregion

    #region Remove Item
    public void RemoveItem(string Name)
    {
        for (int i = 0; i < SlotName.Length; i++)
        {
            if (SlotName[i] == Name)
            {
                SlotName[i] = "Empty";
                SlotFull[i] = false;
                SlotImage[i].sprite = EmptySprite;
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

    #region Use Inventory Item Library
    public void UseItem(int Number)
    {

    }
    #endregion
}
