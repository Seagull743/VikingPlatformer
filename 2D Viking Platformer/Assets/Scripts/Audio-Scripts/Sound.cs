using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float pitchVariance;
    [Range(0f, 1f)]
    public float volumeVariance;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
