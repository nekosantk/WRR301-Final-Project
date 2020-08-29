using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ControlPlayer_SP : MonoBehaviour
{

    private lucy_spells_sp lucy_Spells;

    private GamePlayer_SP gamePlayer;

    // Use this for initialization
    void Start()
    {
        lucy_Spells = GetComponentInParent<lucy_spells_sp>();
        gamePlayer = GetComponentInParent<GamePlayer_SP>();
    }

    void Update()
    {
        LucySpells();
    }
    private void LucySpells()
    {
        if (Input.GetButtonDown("Lucy Charge") && gamePlayer.m_Grounded)
        {
            Debug.Log("Charging!");
            AdjustMana(15);
            lucy_Spells.Charge();
            GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
        }
        if (GetComponentInParent<PlayerInformation_SP>().mana > 0)
        {
            if (Input.GetButtonDown("Lucy Air Attack") && !gamePlayer.m_Grounded)
            {
                Debug.Log("Air Attack!");
                AdjustMana(-20);
                lucy_Spells.Lucy_AirAttack();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
            if (Input.GetButtonDown("Lucy Virgo Attack") && gamePlayer.m_Grounded)
            {
                Debug.Log("Virgo Attack!");
                AdjustMana(-20);
                lucy_Spells.Lucy_VirgoAttack();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
            if (Input.GetButtonDown("Lucy Bull Attack") && gamePlayer.m_Grounded)
            {
                Debug.Log("Bull Attack!");
                AdjustMana(-20);
                lucy_Spells.Lucy_BullAttack();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
            if (Input.GetButtonDown("Lucy Leo Attack") && gamePlayer.m_Grounded)
            {
                Debug.Log("Leo Attack!");
                AdjustMana(-20);
                lucy_Spells.Lucy_LeoAttack();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
    }
    private void AdjustMana(float value)
    {
        GetComponentInParent<PlayerInformation_SP>().UpdateMana(value);
    }
}