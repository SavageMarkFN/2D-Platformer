using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyInventory : MonoBehaviour
{
    #region Variables
    public enum ItemType{Weapon,Armor,Usable,Gathering}

    [Header("Type = Weapon")]
    public Sprite[] WeaponSprite;
    public string[] WeaponName;

    [Header("Type = Armor")]
    public Sprite[] ArmorSprite;
    public string[] ArmorName;

    [Header("Type = Usable")]
    public Sprite[] UsableSprite;
    public string[] UsableName;

    [Header("Type = Gathering")]
    public Sprite[] GatheringSprite;
    public string[] GatheringName;

    [Header("Player Inventory")]
    public Image[] SlotImage;
    private bool[] SlotFull;
    private string[] SlotName;
    public ItemType[] SlotType;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        SlotFull = new bool[SlotImage.Length];
        SlotName = new string[SlotImage.Length];
    }

    public void AddItem(string Type, string Name)
    {
        for (int i = 0; i < SlotImage.Length; i++)
        {
            if (SlotFull[i] == false)
            {
                #region Search Item Image
                if (Type == ItemType.Weapon.ToString())//Weapon
                {
                    for (int j = 0; j < WeaponName.Length; j++)
                    {
                        if (Name == WeaponName[j])
                        {
                            SlotImage[i].sprite = WeaponSprite[j];
                            SlotType[i] = ItemType.Weapon;
                            break;
                        }
                    }
                }
                else if (Type == ItemType.Armor.ToString())//Armor
                {
                    for (int j = 0; j < ArmorName.Length; j++)
                    {
                        if (Name == ArmorName[j])
                        {
                            SlotImage[i].sprite = ArmorSprite[j];
                            SlotType[i] = ItemType.Armor;
                            break;
                        }
                    }
                }
                else if (Type == ItemType.Usable.ToString())//Usable
                {
                    for (int j = 0; j < UsableName.Length; j++)
                    {
                        if (Name == UsableName[j])
                        {
                            SlotImage[i].sprite = UsableSprite[j];
                            SlotType[i] = ItemType.Usable;
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < GatheringName.Length; j++)
                    {
                        if (Name == GatheringName[j])
                        {
                            SlotImage[i].sprite = GatheringSprite[j];
                            SlotType[i] = ItemType.Gathering;
                            break;
                        }
                    }
                }
                #endregion

                SlotFull[i] = true;
                SlotName[i] = Name;
                break;
            }
        }
    }
}
