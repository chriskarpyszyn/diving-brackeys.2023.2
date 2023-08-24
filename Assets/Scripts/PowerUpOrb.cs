using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PowerUpOrb : MonoBehaviour
{
    public GameObject powerupGameObject;
    private string powerupName = "Orb";
    private int maxCooldown = 20;
    private int currentCooldown = 0;

    public void pupBehavior()
    {
        GameObject pow = Instantiate(powerOrb, pos, Quaternion.identity);
        pow.GetComponent<FollowPlayer>().SetPlayer(player);

        Destroy(pow, 0.3f);//TODO extract this number
    }
}
