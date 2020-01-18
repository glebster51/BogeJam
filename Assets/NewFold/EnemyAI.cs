﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float mobSpeed;
    public bool hauntedMob, attackingMob, sayAfter;
    
    public float distance;

    public float eyeDistance = 7f;
    public float attackDistance = 1f;
    
    
    
    private float Facing;
    GameObject PlayerA;
    Animator AnimaMob;
    Rigidbody2D rbm;
    Transform mobVisual;
    // Start is called before the first frame update
    void Start()
    {
        mobSpeed = 1;
        rbm = GetComponent<Rigidbody2D>();
        
       PlayerA = GameObject.FindGameObjectWithTag("Player");
        AnimaMob = GetComponent<EnemyFight>().AnimaMob;
        mobVisual = transform.GetChild(1);

    }

    // Update is called once per frame
    void Update()
    {
        Facing = distance < 0 ? 1 : -1;
        mobVisual.localScale = new Vector3(Facing, 1f, 1f);

        NearingHero();
        if (hauntedMob && !sayAfter)
        {
            AnimaMob.SetBool("Walk", true);

            float movement = Facing * mobSpeed * Time.deltaTime;
            rbm.position += Vector2.right * movement;

            //transform.position += new Vector3(-1 * mobSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            AnimaMob.SetBool("Walk", false);
        }

        if (attackingMob)
        {   
            AnimaMob.SetTrigger("Attack");
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
        distance = transform.position.x - PlayerA.transform.position.x;

        // ==============   Зона чуйки врага ===============
        bool eyeTest = Mathf.Abs(distance) < eyeDistance;
       
        if (!hauntedMob && eyeTest)
            OnFeelPlayer(true);
        else if(hauntedMob && !eyeTest)
            OnFeelPlayer(false);
        
        hauntedMob = eyeTest;
        // ==============   Зона Ближнего боя   =============
        
        bool attackTest = Mathf.Abs(distance) < attackDistance;
        if (!attackingMob && attackTest)
            Attack(true);
        else if (attackingMob && !attackTest)
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
            Debug.Log("Я " + gameObject.name + " АТАКУЮ НАЗУЙ!!!");
        }
        else
        {
            Debug.Log("Я " + gameObject.name + ", НЕ МОГУ ДОТЯНУТЬСЯ, ОН ПРЫТКИЙ СУК...");
        }
    }
    
    
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, eyeDistance);
    }
}

