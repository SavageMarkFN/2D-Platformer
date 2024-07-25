using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIMove : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    public float Speed;
    public Transform[] PatrolPlaces;
    public float WaitForPatrol;
    private bool CanMove = true;
    private float Distance;
    private int CurrentPatrol;

    [Header("Stats")]
    public bool AIFreeze;
    public float Health;
    public bool PhysicalDamage;
    public float Damage;
    public float Armor;
    public float MagicResist;
    [HideInInspector] public bool Dead;

    [Header("References")]
    public UnityEvent DeathEvent;
    [HideInInspector] public GameObject Player;
    [HideInInspector] public PlayerMovement PM;
    [HideInInspector] public Animator animator;
    private AIDetect aiDetect;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("/MaxPrefab/Player");
        PM = Player.GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Movement", 1);
        aiDetect = GetComponentInChildren<AIDetect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AIFreeze == false)
        {
            #region Movement
            if (CanMove == true)
            {
                Distance = (this.transform.position - PatrolPlaces[CurrentPatrol].position).magnitude;
                this.transform.position = Vector2.MoveTowards(this.transform.position, PatrolPlaces[CurrentPatrol].position, Speed);
                this.transform.localScale = PatrolPlaces[CurrentPatrol].localScale;

                if (Distance <= 0.1f)
                {
                    StartCoroutine(NewPatrol());
                }
            }
            #endregion
        }
    }

    #region New Patrol
    IEnumerator NewPatrol()
    {
        CanMove = false;
        animator.SetFloat("Movement", 0);
        CurrentPatrol += 1;
        if (CurrentPatrol > PatrolPlaces.Length - 1)
        {
            CurrentPatrol = 0;
        }
        yield return new WaitForSeconds(WaitForPatrol);
        CanMove = true;
        animator.SetFloat("Movement", 1);
    }
    #endregion

    #region Dealing Damage On Off and Movement Reset
    public void DealingDamageOn()
    {
        aiDetect.HitBox.enabled = true;
        aiDetect.DealingDamage = true;
    }

    public void DealingDamageOff()
    {
        aiDetect.HitBox.enabled = false;
        aiDetect.DealingDamage = false;
    }

    public void MovementReset()
    {
        if (aiDetect.StillClose == false)
        {
            aiDetect.HitBox.enabled = true;
            AIFreeze = false;
        }
        else
        {
            aiDetect.HitBox.enabled = false;
            AIFreeze = true;
            animator.SetTrigger("Attack");
        }
    }
    #endregion

    #region Take Damage
    public void TakeDamage(float Value, bool PhysicalDamage)
    {
        if (Health > 0 && Dead == false)
        {
            if (PhysicalDamage == false)
            {
                float NewValue = Value - Armor;
                if (NewValue > 0)
                {
                    Health -= NewValue;
                }
            }
            else
            {
                float NewValue = Value - MagicResist;
                if (NewValue > 0)
                {
                    Health -= NewValue;
                }
            }

            if (Health <= 0)
            {
                Death();
            }
            else
            {
                animator.SetTrigger("Hit");
            }
        }
    }
    #endregion

    public void Death()
    {
        Dead = true;
        AIFreeze = true;
        animator.SetTrigger("Dead");
        DeathEvent.Invoke();
    }
}
