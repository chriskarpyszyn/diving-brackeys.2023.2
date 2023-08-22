using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject player;

    public Text depthText;
    private int depth;

    public Text healthText;
    private int health;

    public Text velocityText;
    private int velocity;

    public Text powerUpText;
    

    private void Start()
    {
        depthText.text = depth.ToString("0") + "m";
        velocityText.text = "0";
        healthText.text = DrawHearts();
    }

    // Update is called once per frame
    void Update()
    {
        int playerCeilingPosition = -1*(Mathf.CeilToInt(player.transform.position.y) - 1);
        if (depth < playerCeilingPosition)
        {
            depth = playerCeilingPosition;
            depthText.text = depth.ToString("0") + "m";
            FindObjectOfType<GameManager>().SetDepth(depth);
        }

        float fallSpeed = player.GetComponent<PlayerController>().fallSpeed;
        velocityText.text = fallSpeed.ToString("F2");

        healthText.text = DrawHearts();
        powerUpText.text = DrawPowerups();
    }

    private string DrawHearts()
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

    private string DrawPowerups()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        int powerups = gm.GetPowerupCount();
        int nextPowerup = gm.GetNextPowerupCount();
        return "Powerups: " + powerups + " / " + nextPowerup;
    }
}
