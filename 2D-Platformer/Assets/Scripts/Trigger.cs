using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public bool DisableAfter;
    [Space(10)]
    public UnityEvent Event;

    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.name == "Player")
        {
            Event.Invoke();

            if (DisableAfter)
                this.gameObject.SetActive(false);
        }
    }
}
