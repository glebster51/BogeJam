using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float mobSpeed;
    public bool hauntedMob, attackingMob, sayAfter;
    
    public Vector2 distance;

    public float eyeDistance = 7f;
    public float attackDistance = 3f;
    public float attackForce = 10f;
    
    
    
    private float Facing;
    GameObject PlayerA;
    Move playerMover;
    Animator AnimaMob;
    Rigidbody2D rbm;
    Transform mobVisual;
    EnemyFight enemyFight;
    // Start is called before the first frame update
    void Start()
    {
        rbm = GetComponent<Rigidbody2D>();
        
       PlayerA = GameObject.FindGameObjectWithTag("Player");
        playerMover = PlayerA.GetComponent<Move>();
        enemyFight = GetComponent<EnemyFight>();
        AnimaMob = enemyFight.AnimaMob;
        mobVisual = transform.GetChild(1);
    }

    // Update is called once per frame
    public LayerMask groundLayer;
    void Update()
    {
        Facing = distance.x < 0 ? 1 : -1;
        mobVisual.localScale = new Vector3(Facing, 1f, 1f);

        NearingHero();
        if (hauntedMob && !sayAfter  && playerMover.alive)
        {
            AnimaMob.SetBool("Walk", true);
            
            float movement = mobSpeed * Time.deltaTime;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
            Vector2 norm = hit ? hit.normal : Vector2.up;
            //float angleNorm = Mathf.Min(Vector2.Angle(Vector2.up, new Vector2(norm.x * Facing, norm.y)) / 45, 1f);
            float angle = Vector2.Angle(Vector2.up, new Vector2(norm.x * Facing, norm.y));
            float angleNorm = ((int)Mathf.Sign(norm.x) == (int)Facing)? 0f : Mathf.Min(angle / 45, 1f);
            Vector2 dir = Vector2.Lerp(Vector2.right * Facing, (Vector2.right * Facing + Vector2.up), angleNorm);
            Debug.DrawLine(transform.position, transform.position + (Vector3)dir * 10f);
            rbm.velocity = rbm.velocity * (1 - angleNorm);
            rbm.position += dir * movement;

            //transform.position += new Vector3(-1 * mobSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            AnimaMob.SetBool("Walk", false);
        }

        if (attackingMob)
        {   
            AnimaMob.SetBool("Walk", false);
        }
        
        if (sayAfter)
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
        distance = transform.position + Vector3.up - PlayerA.transform.position;

        // ==============   Зона чуйки врага ===============
        bool eyeTest = distance.magnitude < eyeDistance;
       
        if (!hauntedMob && eyeTest)
            OnFeelPlayer(true);
        else if(hauntedMob && !eyeTest)
            OnFeelPlayer(false);
        
        hauntedMob = eyeTest;
        // ==============   Зона Ближнего боя   =============
        
        bool attackTest = distance.magnitude < attackDistance;
        if (!attackingMob && attackTest  && playerMover.alive)
            Attack(true);
        else if (attackingMob && !attackTest )
            Attack(false);
        attackingMob = attackTest;
    }

    private Coroutine stayTimer;
    void OnFeelPlayer(bool feel)
    {
        if (feel)
        {
            Debug.Log("Я " + gameObject.name + " ПИЗДУЮ БЛЯ!!!");
        }
        else
        {
            Debug.Log("Я " + gameObject.name + ", КАЖЕТСЯ ПОКАЗАЛОСЬ...");
            sayAfter = true;

            
            if (stayTimer != null)
                 StopCoroutine(stayTimer);
            stayTimer = StartCoroutine(SayTimer(3f));
        }
        
    }

    
    // Таймер для возможности пиздеть.
    IEnumerator SayTimer(float timer)
    {
        sayAfter = true;
        float t = 0f;
        while (timer > t)
        {
            t += Time.deltaTime;
            yield return null;
        }
        sayAfter = false;
    }

    void Attack(bool startOrEnd)
    {
        if (startOrEnd)
        {
            enemyFight.attackCoroutine = StartCoroutine(PlayerA.GetComponent<BeatEmUpScript>().Attacked(attackForce));
            AnimaMob.SetTrigger("Attack");
            StartCoroutine(AttackingMobCooldown(0.5f));
            Debug.Log("Я " + gameObject.name + " АТАКУЮ НАЗУЙ!!!");
        }
        else
        {
            Debug.Log("Я " + gameObject.name + ", НЕ МОГУ ДОТЯНУТЬСЯ, ОН ПРЫТКИЙ СУК...");
        }
    }

    IEnumerator AttackingMobCooldown(float timer)
    {
        yield return new WaitForSeconds(timer);
        attackingMob = false;
    }
    
    
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, eyeDistance);
    }
}

