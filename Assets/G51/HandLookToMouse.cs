using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HandLookToMouse : MonoBehaviour
{
    public Animator anim;
    private Vector2 mp = new Vector2();
    private void LateUpdate()
    {
        mp = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation *=  Quaternion.FromToRotation(transform.right, mp - (Vector2)transform.position);     ;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
              anim.SetTrigger("Attack");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(mp, 0.1f);
    }
}
