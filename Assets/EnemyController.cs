using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed = 8f;
    public float maxLeft = 10f;
    public float maxRight = 10f;

    public float changedirectionTime = 2f;
    private float timer;
    private int direction = 1;

    void Update()
    {
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        //change direction if it reaches a position >10
        if (transform.position.x < -10 || transform.position.x > 10)
        {
            direction *= -1;
        }

            //change depending on timer        
            timer += Time.deltaTime;
        if (timer > changedirectionTime)
        {
            timer = 0f;
            direction = Random.value < 0.5f ? -1 : 1;
        }

    }
}
