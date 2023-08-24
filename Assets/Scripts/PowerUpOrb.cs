using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpOrb : MonoBehaviour, IPowerup
{
    
    public string powerupName {get; set;}
    public int maxCooldown { get; set; }
    public int currentCooldown { get; set; }
    
    private void Start()
    {
        powerupName = "Orb";
        maxCooldown = 20;
        currentCooldown = 0;
        

    }

    public void pupBehavior(GameObject player)
    {
        GameObject pow = Instantiate(gameObject, transform.position, Quaternion.identity);
        pow.GetComponent<FollowPlayer>().SetPlayer(player);
        Debug.Log(player.transform.position);
        Destroy(pow, 0.3f);//TODO extract this number
    }
}
