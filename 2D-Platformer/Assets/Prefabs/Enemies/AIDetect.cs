using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetect : MonoBehaviour
{
    #region Variables
    [Header("For Detect")]
    private bool DealingDamage;

    [Header("References")]
    private AIMove aiMove;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        aiMove = GetComponent<AIMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiMove.Health <= 0)
        {
            this.enabled = false;
        }
    }

    #region On Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            if (DealingDamage == false && aiMove.AIFreeze == false)
            {
                aiMove.AIFreeze = true;
                aiMove.animator.SetTrigger("Attack");
            }
            else if (aiMove.PM.Health > 0 && DealingDamage == true)
            {
                DealingDamage = false;
                aiMove.PM.TakeDamage(aiMove.Damage, aiMove.PhysicalDamage);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (aiMove.PM.Health > 0 && DealingDamage == true)
            {
                DealingDamage = false;
                aiMove.PM.TakeDamage(aiMove.Damage, aiMove.PhysicalDamage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (aiMove.PM.Health > 0 && DealingDamage == true)
            {
                DealingDamage = false;
                aiMove.PM.TakeDamage(aiMove.Damage, aiMove.PhysicalDamage);
            }
        }
    }
    #endregion

    #region Dealing Damage On Off and Movement Reset
    public void DealingDamageOn()
    {
        DealingDamage = true;
    }

    public void DealingDamageOff()
    {
        DealingDamage = false;
    }

    public void MovementReset()
    {
        aiMove.AIFreeze = false;
    }
    #endregion
}
