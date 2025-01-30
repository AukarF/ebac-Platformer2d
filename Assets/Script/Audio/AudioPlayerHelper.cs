using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPlayerHelper : MonoBehaviour
{
    public AudioSource audioSource;

    public void Play()
    {
        audioSource.Play();
    }
}
