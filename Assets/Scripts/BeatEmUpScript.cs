using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEmUpScript : MonoBehaviour
{
    public float PlayerHP;
    public bool facingRight;
    public Animator playerAnimator;
    private SpriteRenderer mySpriteRenderer;
    public GameObject raybox;
    private Vector2 positionFight;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHP = 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
           // playerAnimator.SetTrigger("Hit");
           
            SendDmg();
        }

        var delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (delta.x >= 0 && !facingRight)
        {
            transform.localScale = new Vector2(1, 1); // or activate look right some other way
            facingRight = true;
            positionFight = Vector2.right;
        }
        else if (delta.x < 0 && facingRight)
        {
            transform.localScale = new Vector2(-1, 1); // activate looking left
            facingRight = false;
            positionFight = Vector2.left;
        }
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(Attacked());
            Debug.Log("Player gets hurt, Health:"+PlayerHP);
        }
    }
    IEnumerator Attacked()
    {
        HurtScript();
        yield return new WaitForSeconds(2f);
    }
    void HurtScript()
    {
        PlayerHP -= 10;
    }

    public void SendDmg()
    {

        RaycastHit2D hit = Physics2D.Raycast(raybox.transform.position, positionFight, 1f);
        
        if (hit) {
        if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Enemy Hit");
            }
        }
    }
}
