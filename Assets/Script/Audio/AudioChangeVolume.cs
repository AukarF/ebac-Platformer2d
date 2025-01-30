using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioChangeVolume : MonoBehaviour
{
    public AudioMixer group;
    public string floatParam = "MyExposedParam";

    public void ChangeValue(float f)
    {
        group.SetFloat(floatParam, f);
    }
}
