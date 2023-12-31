using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public GameObject musicManagerPrefab;
    private static GameObject musicManager;

    //scores and other ui data
    private float scoreDepth = 0;
    private int playerHealth = 3;

    //vars for enemy generation
    public GameObject enemyPrefab;
    private List<GameObject> enemies;
    public int enemyDivider = 5;

    //vars for powerups
    public GameObject powerUpPrefab;
    private int numberOfPowerups = 0;
    private int numberToNextPowerup = 6;
    private int powerUpIncrement = 3;

    private bool gameHasEnded = false;

    private bool canRestart = false;


    void Start()
    {
        Application.targetFrameRate = 60;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //TODO: I can make this better.... kind of janky.
            createEnemies(0, 2);
        }

        if (!GameObject.FindGameObjectWithTag("MusicManager"))
        {
            musicManager = Instantiate(musicManagerPrefab, transform.position, Quaternion.identity);
            musicManager.name = musicManagerPrefab.name;
            DontDestroyOnLoad(musicManager);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && !canRestart)
        {
            StartCoroutine(WaitForRestart());
        }
        if (canRestart && IsAnyKey())
        {
            RestartGame();
        }
    }
    private bool IsAnyKey()
    {
        if (Input.anyKey)
        {
            if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2)) //excluding left, right, and middle mouse buttons
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator WaitForRestart()
    {
        yield return new WaitForSeconds(2); // wait for 3 seconds, for example
        canRestart = true;
    }

    public void SetDepth(int depth)
    {
        scoreDepth = depth/10;
    }

    public float GetDepth()
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
        float depth = scoreDepth;
        //int randomNumber = Random.Range(1, 5); //TODO: I'd like things to be a bit more random...
        int randomNumber = 0;
        int numberOfEnemiesToGenerate = ((int)depth / enemyDivider) + randomNumber;
        float doubleZoneOffset = zoneOffset * 2; //no magic numbers... kinda
        float randomXRange = 9f;

        for (int i = 0; i <= numberOfEnemiesToGenerate; i++)
        {
            float randomX = Random.Range(-randomXRange, randomXRange);
            //int randomYIntRange = Random.Range(-20, 21);
            //float randomY = randomYIntRange * 0.5f;
            float randomY = Random.Range(yPos - doubleZoneOffset + 0.5f, yPos - -zoneOffset - doubleZoneOffset - 0.5f);
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(randomX, randomY, enemyPrefab.transform.position.z), Quaternion.identity);
            Destroy(enemy, 30f);
        }
    }

    private void createPowerups(float yPos, float zoneOffset)
    {
        int randomNumber = Random.Range(0, 1);
        float doubleZoneOffset = zoneOffset * 2;

        float randomXRange = 9f;
        for (int i = 0; i <= randomNumber; i++) {
            float randomX = Random.Range(-randomXRange, randomXRange);
            float randomY = Random.Range(yPos - doubleZoneOffset + 0.5f, yPos - -zoneOffset - doubleZoneOffset - 0.5f); //TODO: is this a typo ypos - -zoneOffset (+?)
            GameObject powerup = Instantiate(powerUpPrefab, new Vector3(randomX, randomY, powerUpPrefab.transform.position.z), Quaternion.identity);
            Destroy(powerup, 30f);
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
