﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] dudes;
    public GameObject player1;
    public float spawnTimer = 10.0f;
    public Vector2 spawnTimerRange;
    private float basecoord;
    public LayerMask groundMask;
    public float spawnDistance;
    float spawnHeight = 50f;


    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            SpawnDudes();
            spawnTimer = Random.Range(spawnTimerRange.x, spawnTimerRange.y);
        }
    }


    public LayerMask raycastTo;
    void SpawnDudes()
    {
        Vector2 leftPoint = (Vector2)player1.transform.position + Vector2.left * spawnDistance;
        Vector2 rightpoint = (Vector2)player1.transform.position + Vector2.right * spawnDistance;
        
        RaycastHit2D leftHitTest = Physics2D.Raycast(leftPoint + Vector2.up * spawnHeight, Vector2.down, 1000f, raycastTo);
        RaycastHit2D rightHitTest = Physics2D.Raycast(rightpoint + Vector2.up * spawnHeight, Vector2.down, 1000f, raycastTo);


        Vector2 spawnPoint;

        bool l = false;
        if (leftHitTest != null)
            l = 1 << leftHitTest.collider.gameObject.layer == groundMask;
        
        bool r = false;
        if (rightHitTest != null)
            r = 1 << rightHitTest.collider.gameObject.layer == groundMask;
        
        if (l && r)
        {
            float side = Mathf.RoundToInt(Random.Range(0f, 1f));
            spawnPoint = Vector2.Lerp(leftHitTest.point, rightHitTest.point, side);
        }
        else if (l || r)
        {
            spawnPoint = l ? leftHitTest.point : rightHitTest.point;
        }
        else
        {
            spawnPoint = player1.transform.position + Vector3.up * 3f;
        }

        GameObject dude = Instantiate(dudes[(Random.Range(0, dudes.Length))], spawnPoint, Quaternion.identity);
        dude.GetComponent<EnemyAI>().eyeDistance = spawnDistance + 5f;
       



        /*
       basecoord = Random.Range(-37, 134);
       Instantiate(dudes[(Random.Range(0, dudes.Length))], new Vector3(basecoord, Random.Range(9, 10), 0), Quaternion.identity);
       SpawnTimer = Random.Range(10.0f, 15.0f);
       */
    }

    private void OnDrawGizmos()
    {
        if (player1)
        {
            Gizmos.color = Color.red;
            Vector2 leftPoint = (Vector2)player1.transform.position + Vector2.left * spawnDistance;
            Vector2 rightpoint = (Vector2)player1.transform.position + Vector2.right * spawnDistance;
            Gizmos.DrawRay(leftPoint + Vector2.up * spawnHeight, Vector3.down * 100f);
            Gizmos.DrawRay(rightpoint + Vector2.up * spawnHeight, Vector3.down * 100f);
        }

    }
}