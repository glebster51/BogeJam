using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatEmUpScript : MonoBehaviour
{
    public float PlayerHP= 100f;
    public float PlayerHP_Max = 100f;
    public float attackForce = 10f;
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
    public HealthBar healthBar;
    public MaterialSwapper matSwap;


    // Start is called before the first frame update
    void Start()
    {
        PlayerHP = PlayerHP_Max;
        healthBar = transform.GetChild(2).GetComponent<HealthBar>();
        playerVisual = transform.GetChild(1);
        matSwap = playerVisual.GetComponent<MaterialSwapper>();
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

    public IEnumerator Attacked(float dmg)
    {
        Debug.Log("Player gets hurt, Health:" + PlayerHP);
        yield return new WaitForSeconds(0.3f);
        HurtScript(dmg);
        matSwap.SetHitMaterial();
        StartCoroutine(SetDefMat());
    }

    IEnumerator SetDefMat()
    {
        yield return new WaitForSeconds(0.2f);
        matSwap.SetDefaultMaterial();
    }
    void HurtScript(float damage)
    {
        PlayerHP -= damage;
        
        if (healthBar)
            healthBar.SetValue(PlayerHP / PlayerHP_Max);
    }
    
    // ==================================================================================================

    public void SendDmg()
    {

        RaycastHit2D hit = Physics2D.Raycast(raybox.transform.position, handsRoot.right, 2.5f, DMGCollider);
        
        if (hit) {
        if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Enemy (" + hit.transform.name + ") Hit");
                hit.transform.GetComponent<EnemyFight>().MobHurt(attackForce);
                
            }
        }
    }
}
