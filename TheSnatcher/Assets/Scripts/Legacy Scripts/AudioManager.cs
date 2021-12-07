using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager audioManager;

    public AudioMixer audioMixer;

    private AudioMixerGroup groups;

    void Awake()
    {
        AudioMixerGroup[] groups = audioMixer.FindMatchingGroups("Master");
        foreach (Sounds s in sounds)
        {
            bool assigned = false;
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;

            foreach (AudioMixerGroup a in groups)
            {
                if (a.name == s.audioMixer)
                {
                    s.source.outputAudioMixerGroup = a;
                    assigned = true;
                }
            }

            if (!assigned)
            {
                s.source.outputAudioMixerGroup = groups[0];
                Debug.Log("Assigned to Master");
            }
        }
    }
    private void Start() //might have problems loading in from scene to scene if we keep a singleton
    {
        if(GameStateManager.m_GameState == GameStateManager.GAMESTATE.Menu)
        {

        }
        if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.FirstLevel)
        {
            PlayAudio("First Level");
        }
        if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.SecondLevel)
        {
            PlayAudio("Second Level");
        }
        if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.ThirdLevel)
        {
            PlayAudio("Third Level");
        }
    }

    public void PlayAudio(string musName)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.name == musName);
        if (s == null)
        {
            Debug.Log("Sound : " + musName + " was not found.");
            return;
        }
        Debug.Log("Playing Audio of " + musName);
        s.source.Play();
    }


}
