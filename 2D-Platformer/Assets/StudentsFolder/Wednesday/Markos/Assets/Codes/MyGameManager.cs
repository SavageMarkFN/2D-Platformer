using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyGameManager : MonoBehaviour
{
    #region Player Variables
    private int UIOpened; //0 = None, 1 = Pause Menu, 2 = Inventory, 3 = Something else

    [Header("References")]
    private PlayerMovement PM;

    [Header("Player UI")]
    public Image PlayerIcon;
    public Slider[] PlayerSlider; //0 = Health, 1 = Mana, 2 = Stamina, 3 = XP
    public TextMeshProUGUI GoldText;

    [Header("Inventory")]
    public GameObject InventoryObject;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.Find("/MaxPrefab/Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerUI();
        InventoryManager();
    }

    #region Player UI
    void SetPlayerUI()
    {
        PlayerIcon.sprite = PM.PlayerIcon;
        //GoldText.text = PM.Gold.ToString();
        PlayerSlider[0].maxValue = PM.MaxHealth;
        PlayerSlider[0].value = PM.Health;
        PlayerSlider[1].maxValue = PM.MaxMana;
        PlayerSlider[1].value = PM.Mana;
        PlayerSlider[2].maxValue = PM.MaxStamina;
        PlayerSlider[2].value = PM.Stamina;
    }
    #endregion

    #region Inventory Manager
    void InventoryManager()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (UIOpened == 0)
            {
                PM.CanAttack = false;
                InventoryObject.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                UIOpened = 2;
            }
            else if (UIOpened == 2)
            {
                PM.CanAttack = true;
                InventoryObject.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                UIOpened = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && UIOpened == 2)
        {
            PM.CanAttack = true;
            InventoryObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            UIOpened = 0;
        }
    }
    #endregion
}
