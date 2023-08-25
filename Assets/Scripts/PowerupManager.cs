using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            ActivatePowerup(powerOrb, 1);
        }
    }

    private void pupOrbBehavior()
    {
        powerOrb.GetComponent<IPowerup>().pupBehavior(player);
    }

    public void ActivatePowerup(GameObject powerup, int numberSlot)
    {
        numberSlot = 1;

        //TODO: refactor for ellegance - shortcuts will bite you in the ass chris
        if (numberSlot == 1)
        {
            pup1Active = true;
            Transform typeTextTransform = pupUI1.transform.Find("PowerupTypeText");
            if (typeTextTransform != null)
            {
                TextMeshProUGUI typeText = typeTextTransform.GetComponent<TextMeshProUGUI>();
                if (typeText != null)
                {
                    typeText.text = powerup.GetComponent<IPowerup>().GetPowerupName();
                }
            }

            Transform cooldownTextTransform = pupUI1.transform.Find("CooldownTime");
            if (cooldownTextTransform != null)
            {
                TextMeshProUGUI cooldownText = cooldownTextTransform.GetComponent<TextMeshProUGUI>();
                if (cooldownText != null)
                {
                    IPowerup powerupComponent = powerup.GetComponent<IPowerup>();
                    int currentCoolDown = powerupComponent.GetCurrentCooldown();
                    int maxCooldown = powerupComponent.GetMaxCooldown();
                    cooldownText.text = currentCoolDown.ToString() + "/" + maxCooldown.ToString();
                }
            }
            pupUI1.SetActive(true);
        }
        else if (numberSlot == 2)
        {
           //todo implement
        }



        
    }
}
