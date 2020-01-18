using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTransform : MonoBehaviour
{
    public float size = 1f;

    private void OnDrawGizmos()
    {
        Transform tr = transform;
        Vector3 o = tr.position;
        Vector3 r = tr.right;
        Vector3 u = tr.up;
        Vector3 f = tr.forward;

        Color col;
        

        col = Color.red;
        Gizmos.color = col;
        Gizmos.DrawLine(o, o + r * size);

        col = Color.green;
        Gizmos.color = col;
        Gizmos.DrawLine(o, o + u * size);

        col = Color.blue;
        Gizmos.color = col;
        Gizmos.DrawLine(o, o + f * size);

        col = Color.white;
        Gizmos.color = col;
        Gizmos.DrawWireSphere(o, size / 10f);
        
        col.a = 0.5f;
        Gizmos.color = col;
        Transform pt = tr.parent;
        if (pt)
           Gizmos.DrawLine(o, pt.position);
    }
}
