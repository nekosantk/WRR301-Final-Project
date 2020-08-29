using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ControlPlayer : NetworkBehaviour {

    private lucy_spells lucy_Spells;
    private natsu_spells natsu_Spells;
    private gray_spells gray_Spells;
    private juvia_spells juvia_Spells;

    private string selectedHero;
    private bool isLocalPlayer;

    private GamePlayer gamePlayer;

	// Use this for initialization
	void Start () {
        selectedHero = GetComponentInParent<GamePlayer>().selectedHero;
        isLocalPlayer = GetComponentInParent<GamePlayer>().isLocalPlayer;
        gamePlayer = GetComponentInParent<GamePlayer>();

        lucy_Spells = GetComponentInParent<lucy_spells>();
        natsu_Spells = GetComponentInParent<natsu_spells>();
        gray_Spells = GetComponentInParent<gray_spells>();
        juvia_Spells = GetComponentInParent<juvia_spells>();
    }

    void Update() {
        if (!isLocalPlayer)
        {
            return;
        }
        switch (selectedHero)
        {
            case "lucy":
                {
                    LucySpells();
                }
                break;
            case "natsu":
                {
                    NatsuSpells();
                }
                break;
            case "elfman":
                {
                    ElfmanSpells();
                }
                break;
            case "erza":
                {
                    ErzaSpells();
                }
                break;
            case "mirajane":
                {
                    MirajaneSpells();
                }
                break;
            case "mystogan":
                {
                    MystoganSpells();
                }
                break;
            case "makarov":
                {
                    MakarovSpells();
                }
                break;
            case "gray":
                {
                    GraySpells();
                }
                break;
            case "jellal":
                {
                    JellalSpells();
                }
                break;
            case "gajeel":
                {
                    GajeelSpells();
                }
                break;
            case "juvia":
                {
                    JuviaSpells();
                }
                break;
        }
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
        if (GetComponentInParent<PlayerInformation_MP>().mana > 0)
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
    private void NatsuSpells()
    {
        if (Input.GetButtonDown("Natsu Charge") && gamePlayer.m_Grounded)
        {
            Debug.Log("Charging!");
            AdjustMana(15);
            natsu_Spells.Charge();
            GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
        }
        if (GetComponentInParent<PlayerInformation_MP>().mana > 0)
        {
            if (Input.GetButtonDown("Natsu Dragonbreath") && gamePlayer.m_Grounded)
            {
                Debug.Log("Dragon Breath Attack!");
                AdjustMana(-20);
                natsu_Spells.Natsu_Dragonbreath();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
    }
    private void ElfmanSpells()
    {
        if (GetComponentInParent<PlayerInformation_MP>().mana <= 0)
        {
            if (Input.GetButtonDown("Elfman Charge"))
            {
                Debug.Log("Charging!");
                AdjustMana(15);
                natsu_Spells.Charge();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
        else
        {

        }
    }
    private void ErzaSpells()
    {
        if (GetComponentInParent<PlayerInformation_MP>().mana <= 0)
        {
            if (Input.GetButtonDown("Erza Charge"))
            {
                Debug.Log("Charging!");
                AdjustMana(15);
                natsu_Spells.Charge();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
        else
        {

        }
    }
    private void MirajaneSpells()
    {
        if (GetComponentInParent<PlayerInformation_MP>().mana <= 0)
        {
            if (Input.GetButtonDown("Mirajane Charge"))
            {
                Debug.Log("Charging!");
                AdjustMana(15);
                natsu_Spells.Charge();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
        else
        {

        }
    }
    private void MystoganSpells()
    {
        if (GetComponentInParent<PlayerInformation_MP>().mana <= 0)
        {
            if (Input.GetButtonDown("Mystogan Charge"))
            {
                Debug.Log("Charging!");
                AdjustMana(15);
                natsu_Spells.Charge();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
        else
        {

        }
    }
    private void MakarovSpells()
    {
        if (GetComponentInParent<PlayerInformation_MP>().mana <= 0)
        {
            if (Input.GetButtonDown("Makarov Charge"))
            {
                Debug.Log("Charging!");
                AdjustMana(15);
                natsu_Spells.Charge();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
        else
        {

        }
    }
    private void GraySpells()
    {
        if (GetComponentInParent<PlayerInformation_MP>().mana <= 0)
        {
            if (Input.GetButtonDown("Gray Charge"))
            {
                Debug.Log("Charging!");
                AdjustMana(15);
                natsu_Spells.Charge();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
        else
        {
            if (Input.GetButtonDown("Gray Icecannon"))
            {
                Debug.Log("Ice Cannon!");
                AdjustMana(-20);
                gray_Spells.Gray_Icecannon();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
    }
    private void GajeelSpells()
    {
        if (GetComponentInParent<PlayerInformation_MP>().mana <= 0)
        {
            if (Input.GetButtonDown("Gajeel Charge"))
            {
                Debug.Log("Charging!");
                AdjustMana(15);
                natsu_Spells.Charge();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
        else
        {
        }
    }
    private void JellalSpells()
    {
        if (GetComponentInParent<PlayerInformation_MP>().mana <= 0)
        {
            if (Input.GetButtonDown("Jellal Charge"))
            {
                Debug.Log("Charging!");
                AdjustMana(15);
                natsu_Spells.Charge();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
        else
        {

        }
    }
    private void JuviaSpells()
    {
        if (GetComponentInParent<PlayerInformation_MP>().mana <= 0)
        {
            if (Input.GetButtonDown("Juvia Charge"))
            {
                Debug.Log("Charging!");
                AdjustMana(15);
                natsu_Spells.Charge();
                GameObject.Find("NetworkManager").GetComponent<ClientManager>().AddSpell();
            }
        }
        else
        {

        }
    }
    private void AdjustMana(float value)
    {
        GetComponentInParent<PlayerInformation_MP>().CmdUpdateMana(value);
    }
}
