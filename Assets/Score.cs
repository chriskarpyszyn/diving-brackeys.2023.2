using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public Transform player;
    public Text depthText;
    private int depth;

    // Update is called once per frame
    void Update()
    {
        int playerCeilingPosition = -1*(Mathf.CeilToInt(player.position.y) - 1);
        if (depth < playerCeilingPosition)
        {
            depth = playerCeilingPosition;
            depthText.text = depth.ToString("0") + "m";
            FindObjectOfType<GameManager>().SetDepth(depth);
        }
    }
}
