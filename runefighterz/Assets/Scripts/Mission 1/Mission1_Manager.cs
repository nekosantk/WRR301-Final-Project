using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1_Manager : MonoBehaviour {
    public GameObject playerObject;

    private GameObject Part1;
    private GameObject Part2;
    private GameObject CrabShipDestroyed;

    [SerializeField]
    private int CurrentWaypoint;
    private Transform Boat;
    private Transform BoatWaypoint1;
    public float boatSpeed = 3;

    void Awake()
    {
        Part1 = GameObject.Find("Part1");
        Part2 = GameObject.Find("Part2");
        CrabShipDestroyed = GameObject.Find("destroyed_crabship");
        Boat = GameObject.Find("Boat1").transform;
        BoatWaypoint1 = GameObject.Find("BoatWaypoint1").transform;
        //BoatWaypoint1 = GameObject.Find("BoatWaypoint2").transform;
        //BoatWaypoint1 = GameObject.Find("BoatWaypoint3").transform;

        CrabShipDestroyed.SetActive(false);
        Part2.SetActive(false);
    }
    void Update()
    {   
        switch (CurrentWaypoint)
        {
            case 1:
                {
                    if (Boat.position == new Vector3(BoatWaypoint1.position.x, BoatWaypoint1.position.y, BoatWaypoint1.position.z))
                    {
                        playerObject.transform.parent = null;
                        CurrentWaypoint = 0;
                    }
                    float step = boatSpeed * Time.deltaTime;
                    Boat.position = Vector3.MoveTowards(Boat.position, BoatWaypoint1.position, step);
                    playerObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(playerObject.transform.position.x + boatSpeed * Time.deltaTime, Boat.position.y));
                }
                break;
            case 2:
                {
                    //if (Boat.position == new Vector3(BoatWaypoint2.position.x, BoatWaypoint2.position.y, BoatWaypoint2.position.z))
                    //{
                    //    playerObject.transform.parent = null;
                    //    CurrentWaypoint = 0;
                    //}
                    //float step = boatSpeed * Time.deltaTime;
                    //Boat.position = Vector3.MoveTowards(Boat.position, BoatWaypoint1.position, step);
                    //playerObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(playerObject.transform.position.x + boatSpeed * Time.deltaTime, Boat.position.y));
                }
                break;
            case 3:
                {
                    //if (Boat.position == new Vector3(BoatWaypoint3.position.x, BoatWaypoint3.position.y, BoatWaypoint3.position.z))
                    //{
                    //    playerObject.transform.parent = null;
                    //    CurrentWaypoint = 0;
                    //}
                    //float step = boatSpeed * Time.deltaTime;
                    //Boat.position = Vector3.MoveTowards(Boat.position, BoatWaypoint1.position, step);
                    //playerObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(playerObject.transform.position.x + boatSpeed * Time.deltaTime, Boat.position.y));
                }
                break;
        }
    }
    public void MoveToWaypoint1()
    {
        playerObject.transform.parent = Boat.transform;
        Debug.Log("Boat moving to waypoint 1");
        CurrentWaypoint = 1;
    }
    public void MoveToWaypoint2()
    {
        playerObject.transform.parent = Boat.transform;
        Debug.Log("Boat moving to waypoint 2");
        CurrentWaypoint = 2;
    }
    public void MoveToWaypoint3()
    {
        playerObject.transform.parent = Boat.transform;
        Debug.Log("Boat moving to waypoint 3");
        CurrentWaypoint = 3;
    }
    public void DestoryCrabhut()
    {
        Debug.Log("Destroying Crabhut");
        GameObject.Find("destructible_crabhut").SetActive(false);
        CrabShipDestroyed.SetActive(true);
    }
    public void FightShip()
    {
        Debug.Log("Fighting Ship");
        Part2.SetActive(true);
    }
    public void DestroyShip()
    {
        Debug.Log("Destroying Ship");
        Part1.SetActive(false);
    }

}
