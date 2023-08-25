using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupSpeed : MonoBehaviour, IPowerup
{

    private string powerupName = "Speed";
    private int maxCooldown = 50;
    private int currentCooldown = 50;
    private bool coolDown = false;


    public void pupBehavior(GameObject player)
    {
        GameObject pow = Instantiate(gameObject, transform.position, Quaternion.identity);
        pow.GetComponent<FollowPlayer>().SetPlayer(player);
        float destroyTime = (float)maxCooldown / 100;

        player.GetComponent<PlayerController>().SetFallSpeed(30,30);

        Destroy(pow, destroyTime);
    }

    public void resetBehavior(GameObject player)
    {
        player.GetComponent<PlayerController>().ResetFallSpeed();
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
