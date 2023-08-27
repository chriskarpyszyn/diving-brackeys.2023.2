using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PowerupManager : MonoBehaviour
{
    public GameObject player;
    public GameObject powerOrb; //TODO: will contain script w/ details
    public GameObject powerSpeed;

    //powerup ui
    public GameObject pupUI1;
    public GameObject pupUI2;

    //powerup logic
    private bool pup1Active = false;
    private bool pup2Active = false;

    private List<GameObject> powerUpList;

    GameManager gm;

    static PlayerAudio playerAudio;


    private void Start()
    {
        //powerups ui
        pupUI1.SetActive(false);
        pupUI2.SetActive(false);

        //initialization
        powerUpList = new List<GameObject>(2);

        gm = FindObjectOfType<GameManager>();

        playerAudio = player.GetComponent<PlayerAudio>();

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

        if (gm == null)
        {
            gm = FindObjectOfType<GameManager>();
        }

        if (!gm.isGameOver())
        {
            Vector3 pos = player.transform.position;
            if (Input.GetKeyDown("1") && pup1Active && !GetIPowerup(powerUpList[0]).GetCooldown())
            {
                StartCoroutine(StartCooldown(1));
                playerAudio.playOrbSound(); //TODO- cant do this if it's randomized

            }
            if (Input.GetKeyDown("2") && pup2Active && !GetIPowerup(powerUpList[1]).GetCooldown())
            {
                StartCoroutine(StartCooldown(2));
                playerAudio.playSpeedSound();
            }


            //temp
            if (Input.GetKeyDown("z"))
            {
                ActivatePowerup(powerOrb, 1);
            }

            if (Input.GetKeyDown("x"))
            {
                ActivatePowerup(powerSpeed, 2);
            }
        }

    }

    IEnumerator StartCooldown(int numKey)
    {
        ExecuteBehavior(numKey);
        GameObject powerup = powerUpList[numKey-1];
        IPowerup pinterface = GetIPowerup(powerup);
        pinterface.SetCooldown(true);

        Transform activeSquareTransform = GetTransform(GetPupUI(numKey), "ActiveSquare");
        activeSquareTransform.gameObject.SetActive(true);
        int timeLeftActiveCooldown = pinterface.GetMaxCooldown();

        float denom = 100f;
        while (timeLeftActiveCooldown > 0)
        {
            float endTime = Time.time + 1f/denom;  // 1f for 1 second interval
            yield return new WaitUntil(() => Time.time >= endTime);
            timeLeftActiveCooldown--;
            pinterface.SetCurrentCooldown(timeLeftActiveCooldown);
            SetCooldownText(powerup, GetPupUI(numKey), timeLeftActiveCooldown);
        }
        activeSquareTransform.gameObject.SetActive(false);
        ResetBehavior(numKey);

        ///////
        Transform coolDownSquareTransform = GetTransform(GetPupUI(numKey), "CooldownSquare");
        coolDownSquareTransform.gameObject.SetActive(true);
        int cooldownTimeleft = pinterface.GetMaxCooldown();
        while (cooldownTimeleft > 0)
        {
            float endTime = Time.time + 1f/denom;  // 1f for 1 second interval
            yield return new WaitUntil(() => Time.time >= endTime);
            cooldownTimeleft--;
            pinterface.SetCurrentCooldown(cooldownTimeleft);
            SetCooldownText(powerup, GetPupUI(numKey), cooldownTimeleft);
        }
        pinterface.SetCooldown(false);
        coolDownSquareTransform.gameObject.SetActive(false);
        pinterface.SetCurrentCooldown(pinterface.GetMaxCooldown()); //TODO: i can create a reset method
        SetCooldownText(powerup, GetPupUI(numKey), pinterface.GetMaxCooldown());
        
    }

    private void ExecuteBehavior(int puSlot)
    {
        powerUpList[puSlot - 1].GetComponent<IPowerup>().pupBehavior(player);
    }

    private void ResetBehavior(int puSlot)
    {
        powerUpList[puSlot - 1].GetComponent<IPowerup>().resetBehavior(player);
    }

    public void ActivatePowerup()
    {
        //TODO - implement random logic here to fill both up.
        if (powerUpList.Count==0)
        {
            ActivatePowerup(powerOrb, 1);
        } else if (powerUpList.Count == 1)
        {
            ActivatePowerup(powerSpeed, 2);
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
