using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateMapOnDetection : MonoBehaviour
{

    public GameObject enemy;

    //TODO: I could move this to the game manager.
    private int zoneOffset = 10;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager gm = FindObjectOfType<GameManager>();

            Transform gameObjectTransform = gameObject.transform;
            Instantiate(gameObject, new Vector3(gameObjectTransform.position.x, gameObjectTransform.position.y - zoneOffset, gameObjectTransform.position.z), Quaternion.identity);

            gm.generateMap(gameObjectTransform.position.y, zoneOffset);

            //destroy this gameobject
            Destroy(gameObject);
        }
    }
}