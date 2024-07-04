using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline1Helper : MonoBehaviour
{
    //Privates

    //Publics
    public Transform PlayerCurrentTransform;
    public Transform PlayerEndTransform;

    //Scripts

    //Animators
    public Animator PlayerAnimator;

    public void ToIdleSignal()
    {
        PlayerAnimator.SetTrigger("Idle");
        PlayerCurrentTransform.position = PlayerEndTransform.position;
    }
}
