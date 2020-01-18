using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEmUpScript : MonoBehaviour
{
    public float PlayerHP;
    public Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHP = 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.B)))
        {
            playerAnimator.SetTrigger("Hit");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Invoke("HurtScript", 0.5f);
        }
    }

    void HurtScript()
    {
        PlayerHP -= 10;
    }
}
