using System.Collections;
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
    float spawnHeight = 100f;


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


    void SpawnDudes()
    {
        Vector2 leftPoint = (Vector2)player1.transform.position + Vector2.left * spawnDistance;
        Vector2 rightpoint = (Vector2)player1.transform.position + Vector2.right * spawnDistance;;
        
        RaycastHit2D leftHitTest = Physics2D.Raycast(leftPoint + Vector2.up * spawnHeight, Vector2.down, 1000f);
        RaycastHit2D rightHitTest = Physics2D.Raycast(rightpoint + Vector2.up * spawnHeight, Vector2.down, 1000f);


        Vector2 spawnPoint;

        bool l = false;
        if (leftHitTest != null)
            l = leftHitTest.collider.gameObject.layer == groundMask;        
        bool r = false;
        if (rightHitTest != null)
            r = leftHitTest.collider.gameObject.layer == groundMask;
        
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
}