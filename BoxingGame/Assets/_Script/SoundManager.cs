using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<SoundManager>();
                if(obj != null) 
                {
                    instance = obj;
                }
                else
                {
                    var newobj = new GameObject().AddComponent<SoundManager>();
                    instance = newobj;
                }
            }
            return instance;
        }
    }

    private AudioSource bgSound;
    public AudioClip[] bgList;

    GameTitleManager gameTitleManager;

    private void Awake()
    {
        var objs = FindObjectsOfType<SoundManager>();
        if(objs.Length != 1) 
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        bgSound = GetComponent<AudioSource>();

        PlayingBackgroundSound(bgList[0]);
    }

    public void PlayingBackgroundSound(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }

    public void ChangeVolume(float bgmVoloume)
    {
        bgSound.volume = bgmVoloume;
    }
}
