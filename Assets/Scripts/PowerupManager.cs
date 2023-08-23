using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public GameObject player;
    public GameObject powerOrb;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        if (Input.GetKeyDown("1"))
        {
            GameObject pow = Instantiate(powerOrb, pos, Quaternion.identity);
            pow.GetComponent<FollowPlayer>().SetPlayer(player);

            Destroy(pow, 0.3f);//TODO extract this number
        }
    }
}