using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline3Helper : MonoBehaviour
{
    //Animators
    public Animator WarriorAnimator;

    private void OnEnable()
    {
        WarriorAnimator.SetTrigger("Run");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
