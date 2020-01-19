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
    public SoundMaster master;
    public AudioSource audio;

    private bool init = false;
    private void Start()
    {
        Init();
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
    }    
    
    public void SayHanted()
    {
        TrySay(sayOnHunted);
    }    

    public void SayAttack()
    {
        TrySay(sayOnAttack);
    }
    
    public void SayGetDamage()
    {
        TrySay(sayOnGetDamage);
    }

    public void SayDead()
    {
        TrySay(sayOnDead);
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
