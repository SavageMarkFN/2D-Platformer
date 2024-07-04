using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTimeline1Helper : MonoBehaviour
{
    //Privates
    [SerializeField] private bool CanSkip;

    //Publics
    public GameObject ThisTimeline;
    public GameObject Player;
    public GameObject PlayerHUD;
    public GameObject SelfTalk1Object;

    //Scripts

    //Animators
    public Animator PlayerAnimator;

    #region Start Routine
    private void Start()
    {
        StartCoroutine(StartingCutScene());
    }

    IEnumerator StartingCutScene()
    {
        yield return new WaitForSeconds(1f);
        CanSkip = true;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (CanSkip == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.J))
            {
                Timeline1Close();
            }
        }
    }

    public void Timeline1Close()
    {
        Player.SetActive(true);
        PlayerHUD.SetActive(true);
        SelfTalk1Object.SetActive(true);
        ThisTimeline.SetActive(false);
    }

    public void ToIdle()
    {
        PlayerAnimator.SetTrigger("Idle");
        Timeline1Close();
    }
}
