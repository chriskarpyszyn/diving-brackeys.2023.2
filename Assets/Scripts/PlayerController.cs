using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //public float maxFallSpeed = -1f;
    //private Rigidbody rb;

    private float _acceleration = 0.5f;
    private float _maxFallSpeed = 8f;

    //new falling code
    public float fallSpeed = 5f;
    private float lastFallSpeed = 0f;
    public float acceleration = 0.3f;
    public float maxFallSpeed = 20f;

    //movement 
    public float moveSpeed = 9f;
    public float maxLeft = 10;
    public float maxRight = 10;

    private bool ground = false;

    static PlayerAudio playerAudio;

    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        playerAudio = GetComponent<PlayerAudio>();
    }


    void Update()
    {
        if (gm==null)
        {
            gm = FindObjectOfType<GameManager>();
        }
        //falling movement
        if (!ground && !gm.isGameOver())
        {
            if (fallSpeed <= maxFallSpeed)
            {
                fallSpeed = Mathf.Min(fallSpeed + acceleration * Time.deltaTime, maxFallSpeed);
                transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);
            }
            else if (fallSpeed > maxFallSpeed)
            {
                fallSpeed = Mathf.Min(fallSpeed + acceleration * Time.deltaTime, maxFallSpeed);
                transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);
            }
        }

        if (!gm.isGameOver())
        {
            //player left/right movement
            float horizontal = Input.GetAxis("Horizontal");
            if (transform.position.x >= -10 && transform.position.x <= 10)
            {
                Vector3 moveDirection = new Vector3(horizontal, 0, 0);
                moveDirection.Normalize();
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
            else
            {
                Vector3 currentPosition = transform.position;
                if (transform.position.x < 0)
                {
                    currentPosition.x += 0.3f; //bump backwards
                }
                else
                {
                    currentPosition.x -= 0.3f;
                }
                transform.position = currentPosition;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager gm = FindObjectOfType<GameManager>();

        Collider triggerCollider = GetComponent<Collider>();
        if (other.CompareTag("Enemy"))
        {
            playerAudio.playEnemySound();
            gm.DecreaseHealth();
            Destroy(other.gameObject);

            //TODO: I'll want to bump the player in the opposite direction instead of destroy.
        }

        if (other.CompareTag("PowerUp"))
        {
            playerAudio.playPowerupPickupSound();
            gm.IncrementPowerups();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Start")) {
            ground = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Start")) {
            ground = false;
        }
    }

    public float GetFallSpeed()
    {
        return fallSpeed;
    }

    public void SetFallSpeed(float maxFallSpeed, float acceleration)
    {
        this.maxFallSpeed = maxFallSpeed;
        this.acceleration = acceleration;
        this.lastFallSpeed = fallSpeed;
    }

    public void ResetFallSpeed()
    {
        maxFallSpeed = _maxFallSpeed;
        acceleration = _acceleration;
        fallSpeed = lastFallSpeed;
    }
}
