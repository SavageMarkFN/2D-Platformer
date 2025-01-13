using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    #region Variables
    [Header("Variables")]
    public bool PhysicalDamage;
    public float Damage;
    private bool DealDamage = true;

    [Header("References")]
    private PlayerMovement PM;
    private BoxCollider2D BC2D;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        PM = GameObject.Find("/MaxPrefab/Player").GetComponent<PlayerMovement>();
        BC2D = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && DealDamage == true)
        {
            DealDamage = false;
            PM.TakeDamage(Damage, PhysicalDamage);
            StartCoroutine(DamageReset());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && DealDamage == true)
        {
            DealDamage = false;
            PM.TakeDamage(Damage, PhysicalDamage);
            StartCoroutine(DamageReset());
        }
    }

    IEnumerator DamageReset()
    {
        BC2D.enabled = false;
        yield return new WaitForSeconds(1f);
        BC2D.enabled = true;
        DealDamage = true;
    }
}
