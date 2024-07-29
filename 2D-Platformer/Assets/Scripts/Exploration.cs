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
    public TextMeshProUGUI UIAreaText;
    public UnityEvent Event;

    [Header("References")]
    private Animator CanvasAnimator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CanvasAnimator = GameObject.Find("/MaxPrefab/Canvas").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIAreaText.text = AreaName;
            CanvasAnimator.SetTrigger("NewArea");
            Event.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
