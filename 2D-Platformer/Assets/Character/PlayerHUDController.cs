using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{
    //Privates
    [SerializeField] private PlayerMovement playerMovement;

    //Publics
    public Slider HealthSlider;
    public Slider ManaSlider;
    public Slider StaminaSlider;

    //Scripts

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthSlider.value = playerMovement.Health;
        ManaSlider.value = playerMovement.Mana;
        StaminaSlider.value = playerMovement.Stamina;
    }
}
