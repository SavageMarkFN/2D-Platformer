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
    public Image[] Slot;
    private bool[] Full;
    private string[] Name;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }


}
