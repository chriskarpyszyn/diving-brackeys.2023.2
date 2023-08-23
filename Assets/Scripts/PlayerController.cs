using System.Collections;
using System.Collections.Generic;
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

    private bool ground = false;


    void Update()
    {
        //falling movement
        if (!ground)
        {
            fallSpeed = Mathf.Min(fallSpeed + acceleration * Time.deltaTime, maxFallSpeed);
            transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);
        }

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
                currentPosition.x += 0.3f; //bump backwards
            } else
            {
                currentPosition.x -= 0.3f;
            }
            transform.position = currentPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager gm = FindObjectOfType<GameManager>();

        Collider triggerCollider = GetComponent<Collider>();
        if (other.CompareTag("Enemy"))
        {
            gm.DecreaseHealth();
            Destroy(other.gameObject);

            //TODO: I'll want to bump the player in the opposite direction instead of destroy.
        }

        if (other.CompareTag("PowerUp"))
        {
            gm.IncrementPowerups();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Start")) {
            ground = true;
            Debug.Log("ground == true");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Start")) {
            ground = false;
            Debug.Log("ground == false");
        }
    }

    public float GetFallSpeed()
    {
        return fallSpeed;
    }
}
