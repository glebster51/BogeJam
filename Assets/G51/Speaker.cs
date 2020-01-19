using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Speaker : MonoBehaviour
{
    public List<AudioClip> sayOnAwake;
    public List<AudioClip> sayOnHunted;
    public List<AudioClip> sayOnAttack;
    public List<AudioClip> sayOnGetDamage;
    public List<AudioClip> sayOnDead;
    public List<AudioClip> randomSaysOnTimer;
    public SoundMaster master;
    public AudioSource audio;

    private bool init = false;

    public bool thisIsMaxim;
    private void Start()
    {
        Init();
        
        if (thisIsMaxim)
            StartCoroutine(MaximRandomSay(Random.Range(10f, 30f)));
    }

    IEnumerator MaximRandomSay(float timer)
    {
       yield return new WaitForSeconds(timer);
        Debug.Log("###################### РАНДОМНАЯ ФРАЗА");
        TrySay(randomSaysOnTimer);
        StartCoroutine(MaximRandomSay(Random.Range(10f, 30f)));
    }

    void Init()
    {
        if (init)
            return;
        
        init = true;
        master = SoundMaster.GetMaster();
        audio = gameObject.AddComponent<AudioSource>();
    }

    private void Awake()
    {
        Init();
        TrySay(sayOnAwake);
        Debug.Log("###################### ПРИ ПОЯВЛЕНИИ ПИЗДАНУТЬ");
    }


    public void SayHanted()
    {
        TrySay(sayOnHunted);
        Debug.Log("###################### ВРАГ УВИДЕЛ МАКСИМКУ");
    }    

    public void SayAttack()
    {
        if (!thisIsMaxim)
        {
            TrySay(sayOnAttack);
        }
        
        Debug.Log("###################### ВРАГ АТАКУЕТ МАКСИМКУ");
    }
    
    public void SayGetDamage()
    {
        if (thisIsMaxim)
        {
            if (Random.Range(0f,100f) <= 20f)
            {
                TrySay(sayOnGetDamage);
            }
        }
        else
        {
            TrySay(sayOnGetDamage);
        }
        
        Debug.Log("###################### ВРАГ ПОЛУЧИЛ ПИЗДЫ");
    }

    public void SayDead()
    {
        TrySay(sayOnDead);
        Debug.Log("###################### ВРАГ ПРИСМЕРТИ");
    }
    
    //==============================================================
    void TrySay(List<AudioClip> list)
    {
        if (master.SpeakerWanaSay(this))
            if (list.Count > 0)
            {
                audio.clip = list[Random.Range(0, list.Count)];
                audio.Play();
            }
    }
}
