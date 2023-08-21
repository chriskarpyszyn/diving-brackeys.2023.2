using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float followSpeed = 3f;


    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            //Vector3 followPosition = target.position + offset;
            Vector3 followPosition = new Vector3(transform.position.x, target.position.y + offset.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, followPosition, followSpeed * Time.deltaTime);

        }
    }
}
