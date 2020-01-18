﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    public float EnemyHP;
    public Animator AnimaMob;
    // Start is called before the first frame update
    void Start()
    {
        EnemyHP = 100;
        AnimaMob = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void MobHurt()
    {
        EnemyHP = EnemyHP - 10;
        AnimaMob.SetTrigger("getDmg");

    }
}
