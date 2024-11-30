using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyGameManager : MonoBehaviour
{
    #region Player Variables
    private PlayerMovement PM;

    [Header("Player UI")]
    public Slider[] PlayerSlider; //0 = Health, 1 = Mana, 2 = Stamina, 3 = XP
    public TextMeshProUGUI GoldText;
    public Image PlayerIcon;
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
}
