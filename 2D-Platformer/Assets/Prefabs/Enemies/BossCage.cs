using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossCage : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    private bool Dead;

    [Header("References")]
    private AIMove aiMove;

    public UnityEvent SpawnEvent;
    public UnityEvent DeathEvent;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        aiMove = GetComponentInChildren<AIMove>();
        aiMove.AIFreeze = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (aiMove.Health <= 0 && Dead == false)
        {
            Dead = true;
            DeathEvent.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Dead == false)
        {
            aiMove.AIFreeze = false;
            SpawnEvent.Invoke();
        }
    }
}
