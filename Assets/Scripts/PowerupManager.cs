using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public GameObject player;
    public GameObject powerOrb;

    //powerup ui
    public GameObject pupUI1;
    public GameObject pupUI2;


    private void Start()
    {
        //powerups ui
        pupUI1.SetActive(false);
        pupUI2.SetActive(false);


    }

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
