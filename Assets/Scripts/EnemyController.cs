using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private float moveSpeed = 6f;

    private float yMoveSpeed = 0.85f;

    public float maxLeft = 10f;
    public float maxRight = 10f;

    public float changedirectionTime = 2f;
    private float timer;
    private int xDirection = 1;
    private int yDirection = 1;



    private float yTimer;


    private float specificY;

    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        xDirection = Random.value < 0.5f ? -1 : 1;
        yDirection = Random.value < 0.5f ? -1 : 1;
        specificY = transform.position.y;
    }

    void Update()
    {
        if (gm == null)
        {
            gm = FindObjectOfType<GameManager>();
        }

        if (!gm.isGameOver())
        {
            transform.Translate(transform.right * xDirection * moveSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * yDirection * yMoveSpeed * Time.deltaTime);

            //change direction if it reaches a position >10
            float fishy_border = 9.3f;
            if (transform.position.x < -fishy_border || transform.position.x > fishy_border)
            {
                xDirection *= -1;
            }
            float yOffset = 0.45f;
            if (transform.position.y < specificY-yOffset || transform.position.y > specificY+yOffset)
            {
                yDirection *= -1;
            }


            ////cute fishies looking in a direction
            //if ((xDirection > 0 && yDirection > 0) || (xDirection < 0 && yDirection < 0))
            //{
            //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 5f);
            //}
            //else
            //{
            //    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -5f);
            //}



            if (xDirection > 0 && yDirection > 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, 5f);
            }
            else if (xDirection > 0 && yDirection < 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180f, -5f);
            }
            else if (xDirection < 0 && yDirection > 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, -5f);
            }
            else if (xDirection < 0 && yDirection < 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 5f);
            }


            //change depending on timer        
            timer += Time.deltaTime;
            if (timer > changedirectionTime)
            {
                changedirectionTime = Random.Range(0.5f, 2f);
                timer = 0f;
                xDirection = Random.value < 0.5f ? -1 : 1;
            }

            //random y timer
            yTimer += Time.deltaTime;
            float randomChangeDirectionTime = 1;
            if (yTimer > randomChangeDirectionTime)
            {
                randomChangeDirectionTime = Random.Range(0.5f, 2f);
                yTimer = 0f;
                yDirection = Random.value < 0.5f ? -1 : 1;
            }
        }
    }
}
