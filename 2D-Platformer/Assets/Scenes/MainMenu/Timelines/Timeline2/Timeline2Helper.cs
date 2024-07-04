using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timeline2Helper : MonoBehaviour
{
    //Animator
    public Animator WarriorAnimator;

    private void OnEnable()
    {
        StartCoroutine(CutScene());
    }

    IEnumerator CutScene()
    {
        WarriorAnimator.SetTrigger("Jump");
        yield return new WaitForSeconds(0.4f);
        WarriorAnimator.SetTrigger("Fall");
        yield return new WaitForSeconds(1.3f);
        WarriorAnimator.SetTrigger("Run");
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(1);
    }
}
