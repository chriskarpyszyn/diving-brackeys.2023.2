using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private GameObject player;
    public float yOffset = 0f;

    void Update()
    {
        transform.position = new Vector3(
            player.transform.position.x, 
            player.transform.position.y + yOffset, 
            player.transform.position.z);
        //transform.position = player.transform.position;
    }

    public void SetPlayer(GameObject p)
    {
        player = p;
    }
}
