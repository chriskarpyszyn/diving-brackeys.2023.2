using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public GameObject player;
    public GameObject powerOrb; //TODO: will contain script w/ details 

    //powerup ui
    public GameObject pupUI1;
    public GameObject pupUI2;

    //powerup logic
    private bool pup1Active = false;
    private bool pup2Active = false;

    private List<GameObject> powerUpList;


    private void Start()
    {
        //powerups ui
        pupUI1.SetActive(false);
        pupUI2.SetActive(false);

        //initialization
        powerUpList = new List<GameObject>(2);


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        if (Input.GetKeyDown("1") && pup1Active)
        {
            pupOrbBehavior();
        }
        if (Input.GetKeyDown("2") && pup2Active)
        {

        }

        if (Input.GetKeyDown("z"))
        {
            pup1Active = true;
            pupUI1.SetActive(true);
        }
    }

    private void pupOrbBehavior()
    {
        powerOrb.GetComponent<IPowerup>().pupBehavior(player);
    }
}
