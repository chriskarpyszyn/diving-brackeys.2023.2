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

    public Canvas healthCanvas;
    public Sprite heartSprite;
    private List<GameObject> heartList;
    

    private void Start()
    {
        heartList = new List<GameObject>();
        depthText.text = depth.ToString("0") + "m";
        velocityText.text = "0";
        AddHeart();
        AddHeart();
        AddHeart();
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

        //DrawHearts();
        powerUpText.text = DrawPowerups();
    }

    private void AddHeart()
    {
        health = FindObjectOfType<GameManager>().GetHealth();
        int currentHearts = heartList.Count;
        int nextHeartIndex = currentHearts + 1;

        GameObject heart = new GameObject("Heart" + nextHeartIndex);
        heart.transform.SetParent(healthCanvas.transform);
        RectTransform rectTransform = heart.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1, 1);
        rectTransform.localScale = new Vector3(10, 10, 10);
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);

        rectTransform.anchoredPosition = new Vector2(7 * (2*nextHeartIndex-1), -7); //TODO: UNIVERSITY HELPS!

        //if (nextHeartIndex == 1)
        //{
        //    rectTransform.anchoredPosition = new Vector2(7 * nextHeartIndex, -7); //TODO: better math.
        //} else
        //{
        //    rectTransform.anchoredPosition = new Vector2(7*nextHeartIndex , -7); //TODO: better math.
        //}
        Image heartImage = heart.AddComponent<Image>();
        heartImage.sprite = heartSprite;
        heartList.Add(heart);
    }

    //TODO: do I want to move this public methid out of here?
    public void RemoveHeart()
    {
        if (heartList.Count <= 0)
        {
            return;
        }
        int currentHearts = heartList.Count;
        GameObject heart = heartList[currentHearts - 1];
        heartList.RemoveAt(currentHearts - 1);
        Destroy(heart);
    }

    private string DrawPowerups()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        int powerups = gm.GetPowerupCount();
        int nextPowerup = gm.GetNextPowerupCount();
        return "Powerups: " + powerups + " / " + nextPowerup;
    }
}
