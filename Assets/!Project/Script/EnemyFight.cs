﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    public float EnemyHP = 100f;
    public float EnemyHP_Max = 100f;
    public Animator AnimaMob;
    public HealthBar healthBar;
    public Coroutine attackCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        EnemyHP = EnemyHP_Max;
        AnimaMob = transform.GetChild(1).GetComponent<Animator>();
        healthBar = transform.GetChild(2).GetComponent<HealthBar>();
        healthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (EnemyHP <= 0)
            Destroy(gameObject);
    }

    public void MobHurt(float damage)
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
        
        EnemyHP -= damage;
        AnimaMob.SetTrigger("getDmg");
        if (healthBar)
            healthBar.SetValue(EnemyHP / EnemyHP_Max);
    }
}