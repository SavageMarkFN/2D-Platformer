using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //Privates
    private bool InPauseMenu;

    //Publics
    public GameObject PauseMenuObject;
    public GameObject PlayerHUD;

    public AudioSource Theme;
    public AudioSource PauseMenuTheme;

    //Scripts
    public PlayerMovement playerMovement;

    //Animator

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.PlayerFreeze == false && Input.GetKeyDown(KeyCode.Escape) && InPauseMenu == false)
        {
            PauseMenuObject.SetActive(true);
            InPauseMenu = true;
            PlayerHUD.SetActive(false);
            playerMovement.PlayerFreeze = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Theme.Stop();
            PauseMenuTheme.Play();
        }
        else if (InPauseMenu == true && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuObject.SetActive(false);
            PlayerHUD.SetActive(true);
            playerMovement.PlayerFreeze = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            InPauseMenu = false;
            Theme.Play();
            PauseMenuTheme.Stop();
        }
    }

    public void ClosePauseMenu()
    {
        PauseMenuObject.SetActive(false);
        PlayerHUD.SetActive(true);
        playerMovement.PlayerFreeze = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        InPauseMenu = false;
        Theme.Play();
        PauseMenuTheme.Stop();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
