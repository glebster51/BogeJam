using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    
    Vector3 refVel = new Vector3();
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 oldPos = transform.position;
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPos = Vector3.Lerp(target.transform.position , mousePos, 0.5F) + offset;
            Vector3 newPos = Vector3.SmoothDamp(oldPos, targetPos, ref refVel, 0.1f);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
    }
}