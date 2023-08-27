using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpOrb : MonoBehaviour, IPowerup
{

    private string powerupName = "Orb";
    private int maxCooldown = 20;
    private int currentCooldown = 20;
    private bool coolDown = false;

    
    public void pupBehavior(GameObject player)
    {
        GameObject pow = Instantiate(gameObject, transform.position, Quaternion.identity);
        pow.GetComponent<FollowPlayer>().SetPlayer(player);
        float destroyTime = (float)maxCooldown / 100;
        Destroy(pow, destroyTime);
    }
    public void resetBehavior(GameObject player)
    {
        //nothing
    }

    public string GetPowerupName()
    {
        return powerupName;
    }

    public int GetMaxCooldown()
    {
        return maxCooldown;
    }

    public int GetCurrentCooldown()
    {
        return currentCooldown;
    }

    public void SetCurrentCooldown(int c)
    {
        currentCooldown = c;
    }

    public void SetCooldown(bool b)
    {
        coolDown = b;
    }
    public bool GetCooldown()
    {
        return coolDown;
    }

    public void PlaySound(GameObject player)
    {
        player.GetComponent<AudioSource>();
    }
}
