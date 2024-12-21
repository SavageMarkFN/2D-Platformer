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

    }
    #endregion

    #region Remove Item
    public void RemoveItem(string Name)
    {

    }
    #endregion

    #region Check For Item
    public void CheckForItem(string Name)
    {

    }
    #endregion

    #region Use Inventory Item Library
    public void UseItem(int Number)
    {

    }
    #endregion
}
