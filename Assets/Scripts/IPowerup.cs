
using UnityEngine;

public interface IPowerup
{
    void pupBehavior(GameObject player);
    string GetPowerupName();
    int GetMaxCooldown();
    int GetCurrentCooldown();
    void SetCurrentCooldown(int c);
}
