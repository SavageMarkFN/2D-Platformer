using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    //Privates
    [SerializeField] private bool CanInteractAgain = false;

    //Publics
    public GameObject IntroUIObject;
    public GameObject Timeline1;
    public GameObject Timeline2;
    public GameObject Timeline3;

    //AudioSources
    public AudioSource IntroAudio;
    public AudioSource MainMenuTheme;

    //Scripts

    //Animators
    public Animator CanvasAnimator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MainMenuOpen());
    }

    IEnumerator MainMenuOpen()
    {
        IntroAudio.Play();
        yield return new WaitForSeconds(3f);
        Timeline1.SetActive(true);
        yield return new WaitForSeconds(2f);
        IntroUIObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        CanvasAnimator.SetTrigger("MainMenuOpen");
        MainMenuTheme.Play();
        CanInteractAgain = true;
    }

    #region MainMenuButtons

    #region StartGame

    //State 1 Press The Start Button
    #region StartButton
    public void StartButtonFunction()
    {
        if (CanInteractAgain == true)
        {
            StartCoroutine(DifficultySelection());
        }
    }

    IEnumerator DifficultySelection()
    {
        CanInteractAgain = false;
        CanvasAnimator.SetTrigger("MainMenuClose");
        CanvasAnimator.SetTrigger("DifficultySelection");
        yield return new WaitForSeconds(2f);
        CanInteractAgain = true;
    }
    #endregion

    //State 2 Select Difficutly
    #region DifficultySelection

    public void DifficultySelectionBackButtonFunction()
    {
        if (CanInteractAgain == true)
        {
            StartCoroutine(ToMainMenuFromDifficulty());
        }
    }

    IEnumerator ToMainMenuFromDifficulty()
    {
        CanInteractAgain = false;
        CanvasAnimator.SetTrigger("DifficultyReturn");
        CanvasAnimator.SetTrigger("MainMenuOpen");
        yield return new WaitForSeconds(2f);
        CanInteractAgain = true;
    }

    #endregion //State 2 Select Difficutly

    //State 3 
    #region StartGame
    public void DifficultySelectionButton(int DifficultySelection)
    {
        if (CanInteractAgain == true)
        {
            StartCoroutine(GameStart());
        }
    }

    IEnumerator GameStart()
    {
        CanInteractAgain = false;
        CanvasAnimator.SetTrigger("DifficultyReturn");
        yield return new WaitForSeconds(1f);
        //Adding the difficulty to the Scriptable Object
        Timeline2.SetActive(true);
    }
    #endregion

    #endregion

    #region OptionsButton

    #endregion

    #region CreditsCode
    public void CreditsButtonFunction()
    {
        if (CanInteractAgain == true)
        {
            StartCoroutine(ToCreditsTag());
        }
    }

    IEnumerator ToCreditsTag()
    {
        CanInteractAgain = false;
        CanvasAnimator.SetTrigger("MainMenuClose");
        CanvasAnimator.SetTrigger("Credits");
        yield return new WaitForSeconds(2f);
        CanInteractAgain = true;
    }

    //Player Press Credits Back Button
    #region CreditsBackButton
    public void CreditsBackButtonFunction()
    {
        if (CanInteractAgain == true)
        {
            StartCoroutine(ToMainMenuTag());
        }
    }

    IEnumerator ToMainMenuTag()
    {
        CanInteractAgain = false;
        CanvasAnimator.SetTrigger("MainMenuOpen");
        yield return new WaitForSeconds(2f);
        CanInteractAgain = true;
    }
    #endregion
    #endregion

    #region QuitButton
    public void QuitButtonFunction()
    {
        if (CanInteractAgain == true)
        {
            StartCoroutine(MainMenuClose());
        }
    }

    IEnumerator MainMenuClose()
    {
        CanInteractAgain = false;
        CanvasAnimator.SetTrigger("MainMenuClose");
        yield return new WaitForSeconds(1f);
        Timeline3.SetActive(true);
    }
    #endregion

    #endregion
}
