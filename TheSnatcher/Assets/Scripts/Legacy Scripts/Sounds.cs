using UnityEngine;
//Josh Castillo
[System.Serializable]
public class Sounds
{
    //info for the sound
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    public string audioMixer;

    public bool mute;

    [HideInInspector]
    public AudioSource source;
}
