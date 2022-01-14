using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfx;
    public AudioSource bgm;

    public static AudioManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<AudioManager>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        instance = this;
        int audioManagerCount = FindObjectsOfType<AudioManager>().Length;
        if (audioManagerCount > 1)
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM()
    {
        if (!bgm.isPlaying)
        {
            bgm.Play();
        }
    }
    public void PlaySFX(int soundToPlay)
    {
        sfx[soundToPlay].Play();
    }

    // Update is called once per frame
    void Update()
    {
        PlayBGM();
    }
}
