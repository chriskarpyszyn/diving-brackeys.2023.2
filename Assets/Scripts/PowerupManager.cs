using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    /***
     * Dumb helper to reduce my typing
     */
    private IPowerup GetIPowerup(GameObject powerUp)
    {
        return powerUp.GetComponent<IPowerup>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        if (Input.GetKeyDown("1") && pup1Active && !GetIPowerup(powerUpList[0]).GetCooldown())
        {
            StartCoroutine(StartCooldown(1));
            ExecuteBehavior(1);
        }
        if (Input.GetKeyDown("2") && pup2Active && !GetIPowerup(powerUpList[1]).GetCooldown())
        {
            StartCoroutine(StartCooldown(2));
            ExecuteBehavior(2);
        }


        //temp
        if (Input.GetKeyDown("z"))
        {
            ActivatePowerup(powerOrb, 1);
        }
    }

    IEnumerator StartCooldown(int numKey)
    {
        GameObject powerup = powerUpList[numKey-1];
        IPowerup pinterface = GetIPowerup(powerup);
        pinterface.SetCooldown(true);
        
        Transform t = GetTransform(GetPupUI(numKey), "CooldownSquare");
        t.gameObject.SetActive(true);
        int timeLeft = pinterface.GetMaxCooldown();
        while (timeLeft > 0)
        {
            float fractionalSeconds = timeLeft / 1000;
            yield return new WaitForSeconds(fractionalSeconds);
            timeLeft--;
            pinterface.SetCurrentCooldown(timeLeft);
            SetCooldownText(powerup, GetPupUI(numKey), timeLeft);
        }
        pinterface.SetCooldown(false);
        t.gameObject.SetActive(false);
        pinterface.SetCurrentCooldown(pinterface.GetMaxCooldown()); //TODO: i can create a reset method
        SetCooldownText(powerup, GetPupUI(numKey), pinterface.GetMaxCooldown());
    }

    private void ExecuteBehavior(int puSlot)
    {
        powerUpList[puSlot - 1].GetComponent<IPowerup>().pupBehavior(player);
    }

    public void ActivatePowerup()
    {
        //TODO - implement random logic here to fill both up.
        if (powerUpList.Count==0)
        {
            ActivatePowerup(powerOrb, 1);
        } else if (powerUpList.Count == 1)
        {
            ActivatePowerup(powerOrb, 2);
        }
        
    }
    private void ActivatePowerup(GameObject powerup, int numberSlot)
    {
        //TODO: refactor for ellegance - shortcuts will bite you in the ass chris
        if (numberSlot == 1)
        {
            powerUpList.Insert(numberSlot-1, powerup);
            pup1Active = true;
            Transform typeTextTransform = GetTransform(pupUI1, "PowerupTypeText");
            if (typeTextTransform != null)
            {
                TextMeshProUGUI typeText = typeTextTransform.GetComponent<TextMeshProUGUI>();
                if (typeText != null)
                {
                    typeText.text = powerup.GetComponent<IPowerup>().GetPowerupName();
                }
            }
            SetCooldownText(powerup, pupUI1, powerup.GetComponent<IPowerup>().GetMaxCooldown());
            pupUI1.SetActive(true);
        }
        else if (numberSlot == 2)
        {
            powerUpList.Insert(numberSlot - 1, powerup);
            pup2Active = true;
            Transform typeTextTransform = GetTransform(pupUI2, "PowerupTypeText");
            if (typeTextTransform != null)
            {
                TextMeshProUGUI typeText = typeTextTransform.GetComponent<TextMeshProUGUI>();
                if (typeText != null)
                {
                    typeText.text = powerup.GetComponent<IPowerup>().GetPowerupName();
                }
            }
            SetCooldownText(powerup, pupUI2, powerup.GetComponent<IPowerup>().GetMaxCooldown());
            pupUI2.SetActive(true);
        }
    }
    /***
     * Helper method to fix some code duplication
     */
    private GameObject GetPupUI(int numKey)
    {
        if (numKey == 1)
        {
            return pupUI1;
        }
        else
        {
            return pupUI2;
        }
    }

    /***
     * A method to set the cooldown text on the powerup boxes.
     */
    private void SetCooldownText(GameObject powerup, GameObject pupUI, int v)
    {
        Transform cooldownTextTransform = GetTransform(pupUI, "CooldownTime");
        if (cooldownTextTransform != null)
        {
            TextMeshProUGUI cooldownText = cooldownTextTransform.GetComponent<TextMeshProUGUI>();
            if (cooldownText != null)
            {
                cooldownText.text = v.ToString();
            }
        }
    }

    /***
     * Helper method to get the transform of the UI elements.
     */
    private Transform GetTransform(GameObject pupUI, String v)
    {
        return pupUI.transform.Find(v);
    }
}
