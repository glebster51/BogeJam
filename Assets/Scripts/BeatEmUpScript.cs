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
    public bool HandChange;
    EnemyFight enema;
    public LayerMask DMGCollider;
    public Transform playerVisual;
    public Transform handsRoot;


    // Start is called before the first frame update
    void Start()
    {
        PlayerHP = 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandChange = !HandChange;
            playerAnimator.SetTrigger("Attack");
            if (HandChange == true) {
                playerAnimator.SetFloat("AttackType", 1f);
            }
            if (HandChange == false)
            {
                playerAnimator.SetFloat("AttackType", 0f);
            }
            SendDmg();
        }

        var delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (delta.x >= 0 && !facingRight)
        {
            playerVisual.localScale = new Vector3(1f, 1f, 1f); // or activate look right some other way
            handsRoot.localScale = new Vector3(1f, 1f, 1f);
            facingRight = true;
            positionFight = Vector2.right;
        }
        else if (delta.x < 0 && facingRight)
        {
            playerVisual.localScale = new Vector2(-1, 1); // activate looking left
            handsRoot.localScale = new Vector3(-1f, -1f, 1f);
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

        RaycastHit2D hit = Physics2D.Raycast(raybox.transform.position, handsRoot.right, 2.5f, DMGCollider);
        
        if (hit) {
        if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Enemy (" + hit.transform.name + ") Hit");
                hit.transform.GetComponent<EnemyFight>().MobHurt();
                
            }
        }
    }
}
