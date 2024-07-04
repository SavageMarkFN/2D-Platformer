using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class AnimController : MonoBehaviour
{
    //Privates
    private bool Died;
    private int Direction;
    private bool CanTakeDamage = true;

    //Publics
    public bool IsMoving;
    public float MovementReset;
    public float HealthReduceDuration;

    //Scripts
    public PlayerMovement playerMovement;

    //Animator
    public Animator WarriorAnimator;

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.PlayerFreeze == false && playerMovement.Health > 0)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                WarriorAnimator.SetFloat("Speed", Mathf.Abs(playerMovement.horizontalMove));
            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                WarriorAnimator.SetFloat("Speed", 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                Direction = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Direction = -1;
            }

            //Animator Variables
            WarriorAnimator.SetBool("Jump", playerMovement.Jump);

            //Attack Check
            WarriorAnimator.SetFloat("Attack", Mathf.Abs(playerMovement.Attack));

            //Dash Check
            WarriorAnimator.SetFloat("Dash", Mathf.Abs(playerMovement.Dash));

            //Slide Check
            WarriorAnimator.SetFloat("Slide", Mathf.Abs(playerMovement.Slide));

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerMovement.Dash = 125 * Direction;
                WarriorAnimator.SetFloat("Dash", Mathf.Abs(playerMovement.Dash));
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.LeftControl))
            {
                playerMovement.Slide = 125 * Direction;
                WarriorAnimator.SetFloat("Slide", Mathf.Abs(playerMovement.Slide));
            }
        }
        
        //Death Check
        if (playerMovement.Death == true && Died == false)
        {
            playerMovement.PlayerFreeze = true;
            Died = true;
            WarriorAnimator.SetTrigger("Death");
        }
    }


    #region Take Damage Code
    public void TakeDamage(float DamageValue)
    {
        StartCoroutine(TakingDamage(DamageValue));
    }

    IEnumerator TakingDamage(float DamageValue)
    {
        if (CanTakeDamage == true && playerMovement.Health > 0)
        {
            CanTakeDamage = false;
            playerMovement.PlayerFreeze = true;
            float DamageTaken = playerMovement.Health - DamageValue;
            float Timer = 0;
            WarriorAnimator.SetTrigger("Hurt");
            while (Timer < HealthReduceDuration)
            {
                Timer += Time.deltaTime;
                float Step = Timer / HealthReduceDuration;

                if (DamageTaken <= 0)
                {
                    playerMovement.Health = Mathf.Lerp(playerMovement.Health, 0, Step);
                }
                else
                {
                    playerMovement.Health = Mathf.Lerp(playerMovement.Health, DamageTaken, Step);
                }

                yield return null;
            }
            //yield return new WaitForSeconds(MovementReset);
            playerMovement.PlayerFreeze = false;
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
}
