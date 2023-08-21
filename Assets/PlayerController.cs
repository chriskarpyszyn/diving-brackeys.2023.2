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
        //float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0, 0);
        moveDirection.Normalize();
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

    }
}
