using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadPeopleCounter : MonoBehaviour
{
    public float killCounter = 0f;
    public Text textOfScore;
    
    void Start()
    {
        killCounter = 0;
    }
    
    void Update()
    {
        //Debug.Log(killCounter);
        textOfScore.text = killCounter.ToString();
    }
}
