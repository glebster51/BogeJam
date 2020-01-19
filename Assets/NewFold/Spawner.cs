using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] dudes;
    GameObject player1;
    private float SpawnTimer = 10.0f;
    private float basecoord;
    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player1 = GameObject.FindGameObjectWithTag("Player");

        SpawnTimer -= Time.deltaTime;

        if (SpawnTimer < 0.01)
        {
            SpawnDudes();
        }
        void SpawnDudes()
        {
            basecoord = Random.Range(-37, 134);
       Instantiate(dudes[(Random.Range(0, dudes.Length))], new Vector3(basecoord, Random.Range(9, 10), 0), Quaternion.identity);
            SpawnTimer = Random.Range(10.0f, 15.0f);
        }
    }
}