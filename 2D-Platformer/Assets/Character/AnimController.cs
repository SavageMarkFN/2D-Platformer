using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class AnimController : MonoBehaviour
{
    #region Variables
    private bool CanTakeDamage = true;

    private PlayerMovement PM;
    [HideInInspector] public Animator animator;
    #endregion

    private void Start()
    {
        PM = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PM.PlayerFreeze == false && PM.Health > 0)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                animator.SetFloat("State", Mathf.Abs(PM.horizontalMove));
            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
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
    //[Header("AttackVariables")]
    //public float[] WaitBeforeHit;
    //public float[] AttackReset;
    //public float[] AttackDamage;
    //public Transform attackPoint;
    //public float attackRange;
    //public LayerMask enemyLayers;
    //public void NormalAttackCall()
    //{
    //    Debug.Log("Normal Attack");
    //    StartCoroutine(NormalAttack());
    //}

    //IEnumerator NormalAttack()
    //{
    //    yield return new WaitForSeconds(WaitBeforeHit[0]);
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); //Check for the enemies
    //    foreach (Collider2D enemy in hitEnemies) //if we hit enemies
    //    {
    //        Debug.Log("We hit" + enemy.name);
    //        //enemy.GetComponent<AIMove>().TakeDamage(AttackDamage[1])
    //        AIMove Enemy = enemy.GetComponent<AIMove>();
    //        BossMove BossEnemy = enemy.GetComponent<BossMove>();

    //        if (Enemy != null)
    //        {
    //            Enemy.TakeDamage(AttackDamage[0]);
    //            Debug.Log("I gave damage");
    //        }
    //        else if (BossEnemy != null)
    //        {
    //            BossEnemy.TakeDamage(AttackDamage[0]);
    //            Debug.Log("I gave damage to the boss");
    //        }
    //    }
    //    yield return new WaitForSeconds(AttackReset[0]);
    //}

    //public void HeavyAttackCall()
    //{
    //    Debug.Log("Heavy Attack");
    //    StartCoroutine(HeavyAttack());
    //}

    //IEnumerator HeavyAttack()
    //{
    //    yield return new WaitForSeconds(WaitBeforeHit[1]);
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); //Check for the enemies
    //    foreach (Collider2D enemy in hitEnemies) //if we hit enemies
    //    {
    //        Debug.Log("We hit" + enemy.name);
    //        //enemy.GetComponent<AIMove>().TakeDamage(AttackDamage[1])
    //        AIMove Enemy = enemy.GetComponent<AIMove>();
    //        BossMove BossEnemy = enemy.GetComponent<BossMove>();

    //        if (Enemy != null)
    //        {
    //            Enemy.TakeDamage(AttackDamage[1]);
    //            Debug.Log("I gave damage");
    //        }
    //        else if (BossEnemy != null)
    //        {
    //            BossEnemy.TakeDamage(AttackDamage[0]);
    //            Debug.Log("I gave damage to the boss");
    //        }
    //    }
    //    yield return new WaitForSeconds(AttackReset[1]);
    //}


    //private void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null)
    //    {
    //        return;
    //    }
    //    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //}
    #endregion

    #region Attack Test Code
    public void DealingDamage()
    {
        Debug.Log("Dealing Damage");
    }

    #endregion

    public void MovementReset()
    {
        PM.MovementReset();
    }
}
