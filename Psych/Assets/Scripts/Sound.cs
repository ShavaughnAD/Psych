using UnityEngine;

//Reference: Brackeys. (2017). Introduction to AUDIO in Unity. Retrieved from https://www.youtube.com/watch?v=6OT43pvUyfY&t=28s
[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip audioClip;

    [Range(0f, 1f)] public float soundVolume;
    [Range(.1f, 3f)] public float soundPitch;

    public bool loopSound;

    [HideInInspector] public AudioSource soundSource;
}
