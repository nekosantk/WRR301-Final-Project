using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInformation_SP : MonoBehaviour {

    public float health = 100;                  // The player's health.
    public float mana = 100;
    public float repeatDamagePeriod = 2f;       // How frequently the player can be damaged.
    public AudioClip[] ouchClips;               // Array of clips to play when the player is damaged.

    public Slider healthBar;           // Reference to the sprite renderer of the health bar.
    public Slider manaBar;
    private float lastHitTime;                  // The time at which the player was last hit.
    private float fillSpeed = 0.5f;

    public GameObject healthPopup;

    public void TakeDamage(int damageAmount)
    {
        GameObject temp = Instantiate(healthPopup, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), transform.rotation);
        temp.GetComponentInChildren<Text>().text = "-" + damageAmount + " HP";
        health -= damageAmount;
        if (health <= 0)
        {
            Death();
        }
    }

    public void GetHealed(int healingAmount)
    {
        GameObject temp = Instantiate(healthPopup, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), transform.rotation);
        temp.GetComponentInChildren<Text>().text = "+" + healingAmount + " HP";
        health += healingAmount;
        if (health > 100)
        {
            health = 100;
        }
        if (health <= 0)
        {
            Death();
        }
    }
    void Update()
    {
        healthBar.value = Mathf.MoveTowards(healthBar.value, health / 100, Time.deltaTime * fillSpeed);
        manaBar.value = Mathf.MoveTowards(manaBar.value, mana / 100, Time.deltaTime * fillSpeed);
    }
    private void Death()
    {
        Time.timeScale = 0f;
        foreach (Transform child in GameObject.Find("Screens").transform)
        {
            if (child.gameObject.name == "loser_screen")
                child.gameObject.SetActive(true);
        }
    }
    public void UpdateMana(float newMana)
    {
        mana += newMana;
        if (mana > 100)
        {
            mana = 100;
        }
        GameObject temp = Instantiate(healthPopup, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), transform.rotation);
        if (newMana <= 0)
        {
            temp.GetComponentInChildren<Text>().text = newMana + " MP";
        }
        else
        {
            temp.GetComponentInChildren<Text>().text = "+" + newMana + " MP";
        }
    }
}
