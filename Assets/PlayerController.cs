using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //public float maxFallSpeed = -1f;
    //private Rigidbody rb;

    //new falling code
    public float fallSpeed = 1f;
    public float acceleration = 0.1f;
    public float maxFallSpeed = 2f;

    //movement 
    public float moveSpeed = 8f;
    public float maxLeft = 10;
    public float maxRight = 10;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        //falling movement
        fallSpeed = Mathf.Min(fallSpeed + acceleration * Time.deltaTime, maxFallSpeed);
        transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);

        //player left/right movement
        float horizontal = Input.GetAxis("Horizontal");
        if (transform.position.x >= -10 && transform.position.x <= 10)
        {
            Vector3 moveDirection = new Vector3(horizontal, 0, 0);
            moveDirection.Normalize();
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        } else
        {
            Vector3 currentPosition = transform.position;
            if (transform.position.x < 0)
            {
                currentPosition.x += 0.3f;
            } else
            {
                currentPosition.x -= 0.3f;
            }
            transform.position = currentPosition;
        }
    

    }
}
