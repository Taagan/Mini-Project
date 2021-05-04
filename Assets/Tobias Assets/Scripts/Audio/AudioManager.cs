using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] m_GameSounds;

    private static Dictionary<string, Sound> m_Sounds;

    private void Awake()
    {
        foreach (Sound sound in m_GameSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            SetAudioSource(sound.source, sound);
        }

        m_Sounds = m_GameSounds.ToDictionary(key => key.Name, value => value);
    }

    private void Start()
    {

    }

    /// <summary>
    /// play sound normally
    /// </summary>
    public static void Play(string name)
    {
        GetSound(name).source.Play();
    }

    /// <summary>
    /// play sound with overlap allowed
    /// </summary>
    public static void PlayOverlap(string name)
    {
        Sound sound = GetSound(name);
        sound.source.PlayOneShot(sound.source.clip);
    }

    /// <summary>
    /// play sound with no overlap allowed
    /// </summary>
    public static void PlaySeperated(string name)
    {
        Sound sound = GetSound(name);

        if (!sound.source.isPlaying)
            sound.source.Play();
    }

    /// <summary>
    /// add AudioSource to object with sound that can be saved and played whenever
    /// </summary>
    public static AudioSource AddSpatialAudioSource(GameObject gameObject, string name, float spatialBlend = 1.0f)
    {
        // Add Audio Source to gameobject to use 3D audio
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        SetAudioSource(audioSource, GetSound(name));
        audioSource.spatialBlend = spatialBlend;

        return audioSource;
    }

    /// <summary>
    /// play a sound at point
    /// </summary>
    public static void PlayAtPoint(Vector3 position, string name, float spatialBlend = 1.0f)
    {
        // Create new empty object at position
        GameObject soundObject = new GameObject();
        soundObject.transform.position = position;
        soundObject.name = "Sound Object";

        // Add audio source to empty object and play sound on it
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();

        SetAudioSource(audioSource, GetSound(name));
        audioSource.spatialBlend = spatialBlend;

        audioSource.Play();

        // Destroy object after sound has finished playing
        Destroy(soundObject, audioSource.clip.length);
    }

    /// <summary>
    /// play a sound at point with no overlap allowed
    /// </summary>
    public static void PlaySeperatedAtPoint(Vector3 position, string name, float spatialBlend = 1.0f)
    {
        Sound sound = GetSound(name);

        if (GameObject.Find(sound.ID))
            return;

        // Create new empty object at position
        GameObject audioObject = new GameObject();
        audioObject.transform.position = position;
        audioObject.name = sound.ID;

        // Add audio source to empty object and play sound on it
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        SetAudioSource(audioSource, sound);
        audioSource.spatialBlend = spatialBlend;

        audioSource.Play();

        // Destroy object after sound has finished playing
        Destroy(audioObject, audioSource.clip.length);
    }

    public static Sound GetSound(string name)
    {
        if (!m_Sounds.ContainsKey(name))
        {
            Debug.LogWarning(name + " does not exist in dictionary");
            return null;
        }

        return m_Sounds[name];
    }

    private static void SetAudioSource(AudioSource audioSource, Sound sound)
    {
        if (audioSource == null)
            return;

        audioSource.clip = sound.Clip;
        audioSource.outputAudioMixerGroup = sound.Output;

        audioSource.volume = sound.Volume;
        audioSource.pitch = sound.Pitch;
        audioSource.loop = sound.Loop;
    }
}
