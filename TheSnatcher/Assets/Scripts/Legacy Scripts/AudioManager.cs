using UnityEngine;
using System;
using UnityEngine.Audio;
//Josh Castillo

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager audioManager;

    public AudioMixer audioMixer;

    private AudioMixerGroup groups;

    void Awake()
    {
        audioManager = this;
        //This gets the groups of Master from the audiomixer
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

            //this will assign the sound to a mixer group based on the name of the AudioMixer assigned to the sound 
            foreach (AudioMixerGroup a in groups)
            {
                //Is the group name the same as the audio mixer name?
                if (a.name == s.audioMixer)
                {
                    s.source.outputAudioMixerGroup = a;
                    assigned = true;
                }
            }
            //if the sound wasn't assigned a group, it will default to master. This is in case something is spelled wrong
            if (!assigned)
            {
                s.source.outputAudioMixerGroup = groups[0];
                Debug.Log("Assigned to Master as no group name was found. Make sure the name is spell correctly");
            }
        }
    }
    //It will play the audio based on GameState the player is in
    private void Start() 
    {
       // Debug.Log(GameStateManager.m_GameState.ToString());
        if(GameStateManager.m_GameState == GameStateManager.GAMESTATE.Menu || GameStateManager.m_GameState == GameStateManager.GAMESTATE.PlayerLost)
        {
            PlayAudio("Main Menu");
        }
        if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.PlayerWon)
        {
            PlayAudio("Main Menu");
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

        //Debug.Log("Playing Audio of " + musName);

        s.source.Play();
    }


}
