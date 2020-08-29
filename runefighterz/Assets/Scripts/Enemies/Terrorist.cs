using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrorist : MonoBehaviour
{
    [SerializeField]
    private Transform player; 
    public float moveSpeed = 3;
    public int damageAmount = 5;
    public float attackTime = 2f;
    public int healthAmount = 100;
    [SerializeField]
    private float hitRange;
    private bool inRange = false;
    private bool facingRight = false;
    private bool lastFacing = false;

    void Start()
    {
        player = GameObject.Find("SP_Player").transform;
        hitRange = Random.Range(1, 100) * 0.01f + 1;
        moveSpeed = moveSpeed * Random.Range(1, 100) * 0.01f + 1f;
        InvokeRepeating("AttackTimer", attackTime, attackTime);
    }

    void Update()
    {
            if (Vector2.Distance(transform.position, player.transform.position) > hitRange)
            {
                //Keep chasing
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                inRange = false;
                GetComponent<Animator>().ResetTrigger("canAttack");
            }
            else
            {
                //In range to attack
                inRange = true;
            }
            //transform.LookAt(player);
            GetComponent<Animator>().SetBool("inRange", inRange);
    }
    void FixedUpdate()
    {
        if ((transform.position.x - player.position.x) < 0)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }
        if (lastFacing != facingRight)
        {
            lastFacing = facingRight;
            Flip();
        }
    }
    private void AttackTimer()
    {
        GetComponent<Animator>().SetTrigger("canAttack");
    }
    private void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void DamagePlayer()
    {
        player.gameObject.GetComponent<PlayerInformation_SP>().TakeDamage(damageAmount);
    }
    public void CleanupTerrorist()
    {
        Debug.Log("Cleaning up Terrorist");
        Destroy(gameObject);
    }
}