using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeatEmUpScript : MonoBehaviour
{
    public float PlayerHP= 100f;
    public float PlayerHP_Max = 100f;
    public float attackForce = 10f;
    public bool facingRight;
    public Animator playerAnimator;
    private SpriteRenderer mySpriteRenderer;
    public GameObject raybox;
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
        healthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandChange = !HandChange;
            playerAnimator.SetTrigger("Attack");
            if (HandChange)
                playerAnimator.SetFloat("AttackType", 1f);
       
            if (!HandChange)
                playerAnimator.SetFloat("AttackType", 0f);
          
            SendDmg();
        }

        var delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (delta.x >= 0 && !facingRight)
        {
            playerVisual.localScale = new Vector3(1f, 1f, 1f); // or activate look right some other way
            handsRoot.localScale = new Vector3(1f, 1f, 1f);
            facingRight = true;
        }
        else if (delta.x < 0 && facingRight)
        {
            playerVisual.localScale = new Vector2(-1, 1); // activate looking left
            handsRoot.localScale = new Vector3(-1f, -1f, 1f);
            facingRight = false;
        }

        RestartScene();
    }

    public IEnumerator Attacked(float dmg)
    {
        if (PlayerHP > 0)
        {
            Debug.Log("Player gets hurt, Health:" + PlayerHP);
            yield return new WaitForSeconds(0.3f);
            HurtScript(dmg);
            matSwap.SetHitMaterial();
            StartCoroutine(SetDefMat());
        }
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

        if (PlayerHP <= 0f)
            PlayerDead();
    }
    
    // ==================================================================================================

    public float attackDistance = 2.5f;
    public void SendDmg()
    {

        RaycastHit2D hit = Physics2D.Raycast(raybox.transform.position, handsRoot.right, attackDistance, DMGCollider);
        
        if (hit) {
        if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Enemy (" + hit.transform.name + ") Hit");
                hit.transform.GetComponent<EnemyFight>().MobHurt(attackForce);
                
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(raybox.transform.position, attackDistance);
    }

    private bool waitToRestartButton;
    void PlayerDead()
    {
        GetComponent<Move>().alive = false;
        playerVisual.gameObject.SetActive(false);
        waitToRestartButton = true;
        UI_manager ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI_manager>();
        ui.ShowDeadScreen(true);
    }

    void RestartScene()
    {
        if (waitToRestartButton)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
