using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed, speedj;

    public Rigidbody2D rb;

    public bool isGrounded;
    public bool canMove;
    public Animator playerAnimator;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame


    void Update()
    {

        Vector3 origin = transform.position + Vector3.up;
        RaycastHit2D hit = Physics2D.CircleCast(origin, 1f, Vector2.down, 1f, ground);
        
        if (hit)
        {
            
            isGrounded = Vector2.Angle(hit.normal, Vector2.up) < 30;
        }
        else
        {
            isGrounded = false;
        }




        if (((Input.GetKeyDown(KeyCode.Space)) && (isGrounded == true)))
        {

            rb.velocity = new Vector2(rb.velocity.x, speedj);
        }

        float h = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(h * speed, rb.velocity.y);
        rb.velocity = Vector2.Lerp(rb.velocity, movement, 1);
        if (Input.GetAxis("Horizontal") != 0)
        {
            playerAnimator.SetBool("run", true);
        }
        else { playerAnimator.SetBool("run", false); }
    }

}
