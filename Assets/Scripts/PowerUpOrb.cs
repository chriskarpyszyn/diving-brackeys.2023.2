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
        Destroy(pow, 0.3f);//TODO extract this number
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
}
