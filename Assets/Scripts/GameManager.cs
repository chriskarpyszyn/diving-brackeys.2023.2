using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //scores and other ui data
    private int scoreDepth = 0;
    private int playerHealth = 3;

    //vars for enemy generation
    public GameObject enemyPrefab;
    public List<GameObject> enemies;
    public int enemyDivider = 5;

    void Start()
    {
        Application.targetFrameRate = 60;
        
        //TODO: I can make this better.... kind of janky.
        createEnemies(0, 2);
    }

    void Update()
    {
        
    }

    public void SetDepth(int depth)
    {
        scoreDepth = depth;
    }

    public int GetDepth()
    {
        return scoreDepth;
    }

    public void DecreaseHealth()
    {
        playerHealth--;
    }

    public int GetHealth()
    {
        return playerHealth;
    }

    public void createEnemies(float yPos, float zoneOffset)
    {
        int depth = scoreDepth;
        //int randomNumber = Random.Range(1, 5); //TODO: I'd like things to be a bit more random...
        int randomNumber = 0;
        int numberOfEnemiesToGenerate = (depth / enemyDivider) + randomNumber;
        float doubleZoneOffset = zoneOffset * 2; //no magic numbers... kinda
        float randomXRange = 9f;

        for (int i = 0; i <= numberOfEnemiesToGenerate; i++)
        {
            float randomX = Random.Range(-randomXRange, randomXRange);
            //int randomYIntRange = Random.Range(-20, 21);
            //float randomY = randomYIntRange * 0.5f;
            float randomY = Random.Range(yPos - doubleZoneOffset + 0.5f, yPos - -zoneOffset - doubleZoneOffset - 0.5f);
            Instantiate(enemyPrefab, new Vector3(randomX, randomY, enemyPrefab.transform.position.z), Quaternion.identity);
        }
    }
}
