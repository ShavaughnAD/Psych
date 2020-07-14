using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public string musicToPlay;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        AudioManager.audioManager.Play(musicToPlay, audioSource);
    }
}