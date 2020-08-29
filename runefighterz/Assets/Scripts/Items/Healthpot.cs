using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpot : MonoBehaviour
{
    public int healthAmount;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
        {
            return;
        }
        //Heal the player
        collider.gameObject.GetComponent<PlayerInformation_SP>().GetHealed(healthAmount);
        Destroy(gameObject);
    }
}