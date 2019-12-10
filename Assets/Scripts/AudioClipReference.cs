using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipReference : MonoBehaviour
{
    public static AudioClipReference instance;
    
    public AudioClip hitSound;
    public AudioClip youWon;
    public AudioClip youLost;
    public AudioClip onButtonHover;
    public AudioClip onButtonClick;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
