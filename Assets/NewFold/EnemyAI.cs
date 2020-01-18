using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float mobSpeed;
    public bool hauntedMob,attackingMob,sayAfter;
    public GameObject PlayerA;
    public float distance;
    public Animator AnimaMob;
    private float Facing;
    public Rigidbody2D rbm;

    public Transform mobTransf;
    // Start is called before the first frame update
    void Start()
    {
        mobSpeed = 1;
        rbm = GetComponent<Rigidbody2D>();
        


    }

    // Update is called once per frame
    void Update()
    {
        if (distance<0)
        {
            Facing = 1;
            mobTransf.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            Facing = -1;
            mobTransf.localScale = new Vector3(1f, 1f, 1f);
        }

        NearingHero();
        if (hauntedMob == true)
        {
            AnimaMob.SetBool("Walk", true);

            Vector2 movement = new Vector2(Facing * mobSpeed, rbm.velocity.y);
            rbm.velocity = Vector2.Lerp(rbm.velocity, movement, 1);

            transform.position += new Vector3(-1 * mobSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            AnimaMob.SetBool("Walk", false);
        }

        if (attackingMob == true)
        {   

            AnimaMob.SetTrigger("Attack");
            AnimaMob.SetBool("Walk", false);
            sayAfter = true;
        }
        else if (sayAfter==true)
        {
            AnimaMob.SetBool("Say", true);
        }
        else
        {
            AnimaMob.SetBool("Say", false);
        }
    }
        
    private void NearingHero()
    {
        distance = this.gameObject.transform.position.x - PlayerA.transform.position.x;

        if (distance < 7f)
        {
            hauntedMob = true;
        }

        if (distance < 1.5f&&distance>0||distance>-1.5&&distance<0)
        {
            attackingMob = true;
        }

       


    }


}

