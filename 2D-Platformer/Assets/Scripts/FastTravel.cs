using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FastTravel : MonoBehaviour
{
    #region Variables
    [Header("Reference")]
    private PlayerMovement PM;
    private Animator CanvasAnimator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.Find("/MaxPrefab/Player").GetComponent<PlayerMovement>();
        CanvasAnimator = GameObject.Find("/MaxPrefab/Canvas").GetComponent<Animator>();
    }

    #region OnTriggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CanvasAnimator.SetTrigger("FastTravelSelection");
            PM.PlayerFreeze = true;
        }
    }
    #endregion

    public void SelectNewScene(int SceneToLoad)
    {
        StartCoroutine(LoadingProcess(SceneToLoad));
    }

    public void Cancel()
    {
        PM.PlayerFreeze = false;
        CanvasAnimator.SetTrigger("CancelFastTravel");
    }

    IEnumerator LoadingProcess(int SceneToLoad)
    {
        CanvasAnimator.SetTrigger("CancelFastTravel");
        CanvasAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        AsyncOperation Operation = SceneManager.LoadSceneAsync(SceneToLoad);
        Operation.allowSceneActivation = false;
        float Progress = 0;
        while (!Operation.isDone)
        {
            Progress = Mathf.MoveTowards(Progress, Operation.progress, Time.deltaTime);

            if (Progress >= 0.9f)
            {
                Progress = 1;
                Operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
