using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bradley_Explosion : MonoBehaviour
{
    public int damageAmount = 30;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
        {
            return;
        }
        collider.gameObject.GetComponent<PlayerInformation_SP>().TakeDamage(damageAmount);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound("explosion_swoosh");
    }
    private void Cleanup()
    {
        Destroy(gameObject);
    }
}