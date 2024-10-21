using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Exploration : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    public string AreaName;
    public UnityEvent Event;

    [Header("References")]
    private UIController UIC;
    private Animator CanvasAnimator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        UIC = GameObject.Find("/MaxPrefab/GameScripts").GetComponent<UIController>();
        CanvasAnimator = GameObject.Find("/MaxPrefab/Canvas").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIC.NewAreaText.text = AreaName;
            CanvasAnimator.SetTrigger("NewArea");
            Event.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
