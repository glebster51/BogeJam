using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public float interpVelocity;
    //public float minDistance;
    //public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    Vector3 refVel = new Vector3();
    void FixedUpdate()
    {
        if (target)
        {/*
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
            */

            Vector3 oldPos = transform.position;
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPos = Vector3.Lerp(target.transform.position , mousePos, 0.5F) + offset;
            Vector3 newPos = Vector3.SmoothDamp(oldPos, targetPos, ref refVel, 0.1f);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
            
            
        }
    }
}