
using UnityEngine;

public interface IPowerup
{
 
    string powerupName { get; set; }
    int maxCooldown { get; set; }
    int currentCooldown { get; set; }


    void pupBehavior(GameObject player);
}
