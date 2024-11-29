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
        
    }
}
