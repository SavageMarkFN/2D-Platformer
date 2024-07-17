using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class AnimController : MonoBehaviour
{
    #region Variables
    private bool CanTakeDamage = true;

    [Header("References")]
    private PlayerMovement PM;
    private InputManager IM;
    [HideInInspector] public Animator animator;
    private AudioSource Audio;
    public AudioClip[] Clip;
    #endregion

    private void Start()
    {
        PM = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        IM = GameObject.Find("/MaxPrefab/GameScripts").GetComponent<InputManager>();
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PM.PlayerFreeze == false && PM.Health > 0)
        {
            if (Input.GetKey(IM.MoveLeft) || Input.GetKey(IM.MoveRight))
            {
                animator.SetFloat("State", Mathf.Abs(PM.horizontalMove));
            }
            else if (Input.GetKeyUp(IM.MoveLeft) || Input.GetKeyUp(IM.MoveRight))
            {
                animator.SetFloat("State", 0);
            }
        }
        
        //Death Check
        if (PM.Death == false && PM.Health <= 0)
        {
            PM.PlayerFreeze = true;
            PM.Death = true;
            animator.SetTrigger("Death");
        }
    }

    #region Take Damage Code
    public void TakeDamage(float DamageValue)
    {
        StartCoroutine(TakingDamage(DamageValue));
    }

    IEnumerator TakingDamage(float DamageValue)
    {
        if (CanTakeDamage == true && PM.Health > 0)
        {
            CanTakeDamage = false;
            PM.PlayerFreeze = true;
            float DamageTaken = PM.Health - DamageValue;
            float Timer = 0;
            animator.SetTrigger("Hurt");
            while (Timer < 0.3f)
            {
                Timer += Time.deltaTime;
                float Step = Timer / 0.3f;

                if (DamageTaken <= 0)
                {
                    PM.Health = Mathf.Lerp(PM.Health, 0, Step);
                }
                else
                {
                    PM.Health = Mathf.Lerp(PM.Health, DamageTaken, Step);
                }

                yield return null;
            }
            //yield return new WaitForSeconds(MovementReset);
            PM.PlayerFreeze = false;
            CanTakeDamage = true;
        }
    }
    #endregion

    #region Attack Code
    [Header("Attack Variables")]
    public Transform AttackPoint;
    public float AttackRange;
    public LayerMask EnemyLayer;

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayer); //Check for the enemies
        foreach (Collider2D enemy in hitEnemies) //if we hit enemies
        {
            Debug.Log("We hit" + enemy.name);
            //enemy.GetComponent<AIMove>().TakeDamage(AttackDamage[1])
            AIMove Enemy = enemy.GetComponent<AIMove>();

            if (Enemy != null)
            {
                Enemy.TakeDamage(PM.Damage, true);
                Debug.Log("I gave damage");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null) return;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
    #endregion

    public void PlayAudio(int Number)
    {
        Audio.clip = Clip[Number];
        Audio.Play();
    }

    public void MovementReset()
    {
        PM.MovementReset();
    }
}
