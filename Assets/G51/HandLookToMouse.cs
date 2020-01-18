using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HandLookToMouse : MonoBehaviour
{
 
    private Vector2 mp = new Vector2();
    private void LateUpdate()
    {
        mp = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 eul = transform.localEulerAngles;
        eul.x = eul.y = 0;
        transform.localEulerAngles = eul;

        transform.rotation *=  Quaternion.FromToRotation(transform.right, mp - (Vector2)transform.position);



        
              
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(mp, 0.1f);
    }
}
