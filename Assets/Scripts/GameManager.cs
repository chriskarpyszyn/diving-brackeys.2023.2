using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    //scores and other ui data
    private int scoreDepth = 0;
    private int playerHealth = 3;

    //vars for enemy generation
    public GameObject enemyPrefab;
    private List<GameObject> enemies;
    public int enemyDivider = 5;

    //vars for powerups
    public GameObject powerUpPrefab;
    public int numberOfPowerups = 0;
    public int numberToNextPowerup = 2;
    private int powerUpIncrement = 1;

    private bool gameHasEnded = false;


    void Start()
    {
        Application.targetFrameRate = 60;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //TODO: I can make this better.... kind of janky.
            createEnemies(0, 2);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && Input.anyKeyDown)
        {
            Debug.Log("why");
            RestartGame();
        }
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
        if (playerHealth>0)
        {
            playerHealth--;
            FindObjectOfType<UIController>().RemoveHeart(); //TODO: do I want to move this code elsewhere?
        }
        if (playerHealth==0)
        {
            EndGame();
        }

    }

    public int GetHealth()
    {
        return playerHealth;
    }

    public void generateMap(float ypos, float zoneOffset)
    {
        createEnemies(ypos, zoneOffset);
        createPowerups(ypos, zoneOffset);
    }

    public int GetPowerupCount()
    {
        return numberOfPowerups;
    }

    public int GetNextPowerupCount()
    {
        return numberToNextPowerup;
    }

    public void IncrementPowerups()
    {
        numberOfPowerups++;

        if (numberOfPowerups >= numberToNextPowerup)
        {
            numberOfPowerups = 0;
            numberToNextPowerup = numberToNextPowerup + powerUpIncrement;
            //TODO improve on this simplfiied difficulty curve.
            powerUpIncrement = powerUpIncrement + 1 + (powerUpIncrement / 2);

            GetComponent<PowerupManager>().ActivatePowerup();
        }
    }

    private void createEnemies(float yPos, float zoneOffset)
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

    private void createPowerups(float yPos, float zoneOffset)
    {
        int randomNumber = Random.Range(0, 3);
        float doubleZoneOffset = zoneOffset * 2;

        float randomXRange = 9f;
        for (int i = 0; i <= randomNumber; i++) {
            float randomX = Random.Range(-randomXRange, randomXRange);
            float randomY = Random.Range(yPos - doubleZoneOffset + 0.5f, yPos - -zoneOffset - doubleZoneOffset - 0.5f); //TODO: is this a typo ypos - -zoneOffset (+?)
            Instantiate(powerUpPrefab, new Vector3(randomX, randomY, powerUpPrefab.transform.position.z), Quaternion.identity);
        }
    }

    private void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            SceneManager.LoadScene(1);
        }
    }

    private void RestartGame()
    {
        gameHasEnded = false;
        SceneManager.LoadScene(0);
    }

    public bool isGameOver()
    {
        return gameHasEnded;
    }
}
