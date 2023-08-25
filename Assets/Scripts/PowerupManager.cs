using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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

    private bool pup1Cooldown = false;

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
        if (Input.GetKeyDown("1") && pup1Active && !pup1Cooldown)
        {
            StartCoroutine(StartCooldown(powerOrb)); //todo hacking object directly in
            ExecuteBehavior(1);
            
            
        }
        if (Input.GetKeyDown("2") && pup2Active)
        {
            //to implement
        }

        //if (pup1Cooldown)
        //{
        //    IPowerup powerupComponent = powerUpList[puSlot - 1].GetComponent<IPowerup>();
        //    int currentCoolDown = powerupComponent.GetCurrentCooldown();
        //}

        //temp
        if (Input.GetKeyDown("z"))
        {
            ActivatePowerup(powerOrb, 1);
        }
    }

    IEnumerator StartCooldown(GameObject powerup)
    {
        Debug.Log("do I enter");
        pup1Cooldown = true;
        //todo i can extract this
        Transform t = GetTransform(pupUI1, "CooldownSquare");
        t.gameObject.SetActive(true);
        int timeLeft = powerup.GetComponent<IPowerup>().GetMaxCooldown();
        while (timeLeft > 0)
        {
            Debug.Log("do I party?");
            yield return new WaitForSeconds(0.02f);
            timeLeft--;
            powerup.GetComponent<IPowerup>().SetCurrentCooldown(timeLeft);
            SetCooldownText(powerup, timeLeft);
        }
        Debug.Log("do I exit?");
        pup1Cooldown = false;
        t.gameObject.SetActive(false);
        //TODO hacking for now to see if it works
        powerup.GetComponent<IPowerup>().SetCurrentCooldown(20);
        SetCooldownText(powerup, 20);
    }

    private void ExecuteBehavior(int puSlot)
    {
        powerUpList[puSlot - 1].GetComponent<IPowerup>().pupBehavior(player);
    }

    public void ActivatePowerup(GameObject powerup, int numberSlot)
    {
        numberSlot = 1;

        //TODO: refactor for ellegance - shortcuts will bite you in the ass chris
        if (numberSlot == 1)
        {
            powerUpList.Insert(numberSlot-1, powerup);
            pup1Active = true;
            Transform typeTextTransform = pupUI1.transform.Find("PowerupTypeText"); //use new method
            if (typeTextTransform != null)
            {
                TextMeshProUGUI typeText = typeTextTransform.GetComponent<TextMeshProUGUI>();
                if (typeText != null)
                {
                    typeText.text = powerup.GetComponent<IPowerup>().GetPowerupName();
                }
            }
            SetCooldownText(powerup, 20);
            pupUI1.SetActive(true);
        }
        else if (numberSlot == 2)
        {
           //todo implement
        }
    }

    private void SetCooldownText(GameObject powerup, int v)
    {
        Transform cooldownTextTransform = pupUI1.transform.Find("CooldownTime");
        if (cooldownTextTransform != null)
        {
            TextMeshProUGUI cooldownText = cooldownTextTransform.GetComponent<TextMeshProUGUI>();
            if (cooldownText != null)
            {
                cooldownText.text = v.ToString();
            }
        }
    }

    private Transform GetTransform(GameObject pupUI, String v)
    {
        return pupUI.transform.Find(v);
    }
}
