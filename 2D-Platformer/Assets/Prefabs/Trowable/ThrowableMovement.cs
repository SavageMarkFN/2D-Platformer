using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableMovement : MonoBehaviour
{
    #region Variables
    private Vector2 NewPlace;
    private bool IsFacingRight;
    public float Speed;

    private bool Called;
    [Space(10)]
    public float DestroyTime;

    PlayerMovement PM;
    #endregion

    private void Start()
    {
        PM = GameObject.Find("/MaxPrefab/Player").GetComponent<PlayerMovement>();
        IsFacingRight = PM.isFacingRight;
        if (PM.isFacingRight)
        {
            Vector3 localScales = transform.localScale;
            localScales.x *= 1;
            transform.localScale = localScales;
        }
        else
        {
            Vector3 localScales = transform.localScale;
            localScales.x *= -1;
            transform.localScale = localScales;
        }
    }

    void Update()
    {
        //Moves the arrow
        if (IsFacingRight)
            NewPlace = new Vector2(this.transform.position.x + 2, this.transform.position.y);
        else
            NewPlace = new Vector2(this.transform.position.x + 2 * -1, this.transform.position.y);
        this.transform.position = Vector2.MoveTowards(this.transform.position, NewPlace, Time.deltaTime * Speed);

        //Starts a Coroutine
        StartCoroutine(Destroy());
        Hit();

        if (Check())
            Destroy(this.gameObject);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(DestroyTime);
        Destroy(this.gameObject);
    }

    public LayerMask CheckLayer;
    bool Check()
    {
        return Physics2D.OverlapCircle(this.transform.position, 0.1f, CheckLayer);
    }

    #region Attack Code
    [Header("Attack Variables")]
    public Transform AttackPoint;
    public float AttackRange;
    public LayerMask EnemyLayer;
    bool HitEnemy;

    public void Hit()
    {
        HitEnemy = false;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayer); //Check for the enemies
        foreach (Collider2D enemy in hitEnemies) //if we hit enemies
        {
            Debug.Log("We hit" + enemy.name);
            //enemy.GetComponent<AIMove>().TakeDamage(AttackDamage[1])
            AIMove Enemy = enemy.GetComponent<AIMove>();

            if (Enemy != null && HitEnemy == false)
            {
                HitEnemy = true;
                Enemy.TakeDamage(PM.Damage, true);
                Debug.Log("I gave damage");
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null) return;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
    #endregion
}
