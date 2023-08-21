using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Transform player;

    public Text depthText;
    private int depth;

    public Text healthText;
    private int health;

    private void Start()
    {
        depthText.text = depth.ToString("0") + "m";
        healthText.text = drawHearts();
    }

    // Update is called once per frame
    void Update()
    {
        int playerCeilingPosition = -1*(Mathf.CeilToInt(player.position.y) - 1);
        if (depth < playerCeilingPosition)
        {
            depth = playerCeilingPosition;
            depthText.text = depth.ToString("0") + "m";
            FindObjectOfType<GameManager>().SetDepth(depth);

            healthText.text = drawHearts();
            

        }
    }

    private string drawHearts()
    {
        health = FindObjectOfType<GameManager>().GetHealth();
        string heartEmoji = "\u2764\uFE0F";
        string temp = "";
        for (int i = 0; i < health; i++)
        {
            temp = temp + " " + heartEmoji;
        }
        return temp;
    }
}
