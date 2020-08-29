using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Information : MonoBehaviour
{
    public GameObject healthPot;
    public GameObject healthPopup;
    public int healthAmount = 20;
    public bool alive = true;

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Taking damage");
        healthAmount -= damageAmount;
        GameObject temp = Instantiate(healthPopup, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), transform.rotation);
        temp.GetComponentInChildren<Text>().text = "-" + damageAmount + " HP";
        if (healthAmount <= 0)
        {
            alive = false;
            GetComponent<Animator>().SetBool("aliveState", false);
            Instantiate(healthPot, transform.position, transform.rotation);
        }
    }
}