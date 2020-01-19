using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaster : MonoBehaviour
{
    static SoundMaster instance = null;

    public static SoundMaster GetMaster()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<SoundMaster>();
            if (instance == null)
                instance = new GameObject("SoundMaster").AddComponent<SoundMaster>();
        }
        return instance;
    }



    List<Speaker> activeSpeakers = new List<Speaker>();
    int maximumSpeakersOnline = 3;



    public bool SpeakerWanaSay(Speaker speaker)
    {
        bool canSay = (activeSpeakers.Count < maximumSpeakersOnline);
        if (canSay)
        {
            activeSpeakers.Add(speaker);
            StartCoroutine(SpeakerSayTimer(speaker, 2f));
        }

        return canSay;
    }

    IEnumerator SpeakerSayTimer(Speaker speaker, float timer)
    {
        yield return new WaitForSeconds(timer);
        activeSpeakers.Remove(speaker);
    }
}
