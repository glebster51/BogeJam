using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadPeopleCounter : MonoBehaviour
{
    public float killCounter;
    public Text textOfScore;
    // Start is called before the first frame update
    void Start()
    {
        killCounter = 0;
        textOfScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(killCounter);
        textOfScore.text = killCounter.ToString();



    }
}
