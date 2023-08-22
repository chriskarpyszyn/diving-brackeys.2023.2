using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GenerateMapOnDetection : MonoBehaviour
{

    public GameObject enemy;

    private int zoneOffset = 10;
    public int enemyDivider = 5;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hi there");
            GameManager gm = FindObjectOfType<GameManager>();

            Transform gameObjectTransform = gameObject.transform;
            Instantiate(gameObject, new Vector3(gameObjectTransform.position.x, gameObjectTransform.position.y - zoneOffset, gameObjectTransform.position.z), Quaternion.identity);


            
            int depth = gm.GetDepth();
            int randomNumber = Random.Range(1, 4);
            int numberOfEnemiesToGenerate = (depth / enemyDivider) + randomNumber;

            for (int i = 0; i <= numberOfEnemiesToGenerate; i++)
            {
                float randomX = Random.Range(-9.99f, 9.99f);
                float randomY = Random.Range(gameObjectTransform.position.y + 0.01f, gameObjectTransform.position.y - zoneOffset - 0.01f);
                Instantiate(enemy, new Vector3(randomX, randomY, enemy.transform.position.z), Quaternion.identity);
            }


            //destroy this gameobject
            Destroy(gameObject);
        }
    }
}