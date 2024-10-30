using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class My_Inventory : MonoBehaviour
{
    private bool Check;

    [Header("Inventory")]
    public int Available;
    public Image[] Slot;
    private bool[] Full;
    private string[] Name;

    [Header("Items")]
    public Sprite EmptySprite;
    public Sprite[] ItemImage;
    public string[] ItemName;

    // Start is called before the first frame update
    void Start()
    {
        Full = new bool[Slot.Length];
        Name = new string[Slot.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region Add Item
    public void AddItem(string Item)
    {
        for (int i = 0; i < Slot.Length; i++)
        {
            if (Full[i] == false)
            {
                for (int j = 0; j < ItemName.Length; j++)
                {
                    if (ItemName[j] == Item)
                    {
                        Slot[i].sprite = ItemImage[j];
                        Full[i] = true;
                        Name[i] = ItemName[j];
                        Available -= 1;
                        break;
                    }
                }
            }
        }
    }
    #endregion

    #region  Remove Item
    public void RemoveItem(int Number)
    {
        Slot[Number].sprite = EmptySprite;
        Full[Number] = false;
        Name[Number] = "Empty";
        Available += 1;
    }
    #endregion

    #region Check for item
    public void CheckForItem(string Item)
    {
        Check = false;
        for (int i = 0; i < Slot.Length; i++)
        {
            if (Name[i] == Item)
            {
                Check = true;
                break;
            }
        }
    }
    #endregion
}
