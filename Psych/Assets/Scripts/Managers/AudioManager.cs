using UnityEngine;
using UnityEngine.Audio;
using System;

//Reference: Brackeys. (2017). Introduction to AUDIO in Unity. Retrieved from https://www.youtube.com/watch?v=6OT43pvUyfY&t=28s
public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    public Sound[] sounds;

    void Awake()
    {
        if(audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Debug.LogWarning("Another AudioManager exists, destroying myself." + " Name of gameObject is: " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.soundSource = gameObject.AddComponent<AudioSource>();
            AudioMixer mixer = Resources.Load("MainMixer") as AudioMixer;
            string _OutputMixer = "Master";
            s.soundSource.outputAudioMixerGroup = mixer.FindMatchingGroups(_OutputMixer)[0];
            s.soundSource.clip = s.audioClip;
            s.soundSource.volume = s.soundVolume;
            s.soundSource.pitch = s.soundPitch;
            s.soundSource.loop = s.loopSound;
        }
    }

    public void Play(string name)
    {
        //When playing sounds, call this function
        //Syntax is FindObjectOfType<AudioManager>().Play("INSERT NAME");

        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!" + ". Check spelling maybe???");
            return;
        }
        s.soundSource.Play();
    }
}
