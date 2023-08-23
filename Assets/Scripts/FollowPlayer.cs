using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private GameObject player;

    void Update()
    {
        transform.position = player.transform.position;
    }

    public void SetPlayer(GameObject p)
    {
        player = p;
    }
}
