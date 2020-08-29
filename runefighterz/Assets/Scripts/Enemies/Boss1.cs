using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour {

    public int damageAmount = 5;
    public float attackTime = 2f;

    public GameObject fireEffect;
    public GameObject cannonBall;
    public GameObject shellCasing;
    public GameObject firePort;
    public GameObject casingPort;

    void Start () {
        InvokeRepeating("AttackTimer", attackTime, attackTime);
    }

    private void AttackTimer()
    {
        GetComponent<Animator>().SetTrigger("canAttack");
    }
    public void DamagePlayer()
    {
       GameObject.Find("SP_Player").GetComponent<PlayerInformation_SP>().TakeDamage(damageAmount);
    }
    public void Cleanup()
    {
        Debug.Log("Cleaning up boss 1");
        Destroy(gameObject);
    }
    public void ShootBall()
    {
        Debug.Log("Firing cannon");
        GameObject temp = Instantiate(fireEffect, firePort.transform);
        GameObject temp2 = Instantiate(cannonBall, firePort.transform);
        temp.transform.position = new Vector3(firePort.transform.position.x, firePort.transform.position.y, firePort.transform.position.z);
        temp2.transform.position = new Vector3(firePort.transform.position.x, firePort.transform.position.y, firePort.transform.position.z);
    }
    public void EjectCasing()
    {
        Debug.Log("Ejecting casing");
        GameObject temp = Instantiate(shellCasing, casingPort.transform);
        temp.transform.position = new Vector3(casingPort.transform.position.x, casingPort.transform.position.y, casingPort.transform.position.z);
    }
}
