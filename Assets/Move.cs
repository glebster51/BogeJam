using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed, speedj;
    
    public Rigidbody2D rb;
   
    public bool isGrounded;
   
    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    
    void Update()
    {
        
        if (((Input.GetKeyDown(KeyCode.Space)) && (isGrounded == true)))
        {

            rb.velocity = new Vector2(rb.velocity.x, speedj);
        }

        float h = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(h * speed, rb.velocity.y);
        rb.velocity = Vector2.Lerp(rb.velocity, movement, 1);


    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

}
