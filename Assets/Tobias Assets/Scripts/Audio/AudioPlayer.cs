using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// is used to store and easily play different kinds of sounds
/// </summary>
public class AudioPlayer : MonoBehaviour
{
    [SerializeField, Space(3), Tooltip("Which sound to play at start of scene from game sounds")] 
    private string m_StartMusic;

    [SerializeField, Space(5)] 
    private List<Sound> m_GameSounds;

    private static Dictionary<string, Sound> m_Sounds;

    private static AudioPlayer Instance { get; set; } = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound sound in m_GameSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            SetAudioSource(sound.source, sound);
        }

        m_Sounds = m_GameSounds.ToDictionary(key => key.Name, value => value);

        m_GameSounds.Clear(); // no longer needed
        m_GameSounds = null;
    }

    private void Start()
    {
        Play(m_StartMusic);
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
        // add Audio Source to gameobject to use 3D audio
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        SetAudioSource(audioSource, GetSound(name));
        audioSource.spatialBlend = spatialBlend;

        return audioSource;
    }

    /// <summary>
    /// play a sound at point in world
    /// </summary>
    public static void PlayAtPoint(string name, Vector3 position, float spatialBlend = 1.0f)
    {
        Sound sound = GetSound(name);

        // Create new empty object at position
        GameObject soundObject = new GameObject();
        soundObject.transform.position = position;
        soundObject.name = sound.ID;

        // Add audio source to empty object and play sound on it
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();

        SetAudioSource(audioSource, sound);
        audioSource.spatialBlend = spatialBlend;

        audioSource.Play();

        // Destroy object after sound has finished playing
        Destroy(soundObject, audioSource.clip.length);
    }

    /// <summary>
    /// play a sound at point in world with no overlap allowed
    /// </summary>
    public static void PlaySeperatedAtPoint(string name, Vector3 position, float spatialBlend = 1.0f)
    {
        Sound sound = GetSound(name);

        if (GameObject.Find(sound.ID)) // if object exists means the sound is already playing
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

    // TODO: COROUTINES NEEDS MORE TESTING

    /// <summary>
    /// play a sound with delay in seconds
    /// </summary>
    public static void PlayWithDelay(string name, float delay)
    {
        Instance.StartCoroutine(Instance.PlayWithDelay(GetSound(name), delay));
    }

    /// <summary>
    /// play a sound with overlap allowed with delay in seconds
    /// </summary>
    public static void PlayOverlapWithDelay(string name, float delay)
    {
        Instance.StartCoroutine(Instance.PlayOverlapWithDelay(GetSound(name), delay));
    }

    /// <summary>
    /// play a sound seperated with delay in second
    /// </summary>
    public static void PlaySeperatedWithDelay(string name, float delay)
    {
        Instance.StartCoroutine(Instance.PlaySeperatedWithDelay(GetSound(name), delay));
    }

    /// <summary>
    /// play a sound at point with delay in seconds
    /// </summary>
    public static void PlayWithDelayAtPoint(string name, float delay, Vector3 position, float spatialBlend = 1.0f)
    {
        Instance.StartCoroutine(Instance.PlayWithDelayAtPoint(GetSound(name), delay, position, spatialBlend));
    }

    /// <summary>
    /// add custom sound to be stored and used in audioplayer
    /// </summary>
    public static bool AddSound(string name, AudioSource source)
    {
        Sound sound = new Sound(name, source);

        if (!m_Sounds.ContainsKey(name))
        {
            m_Sounds.Add(name, sound);
            return true;
        }
        else
            Debug.LogWarning("cannot add '" + name + "', it already exists");

        return false;
    }

    /// <summary>
    /// set custom audio source for sound
    /// </summary>
    public static void SetSound(string name, AudioSource source)
    {
        GetSound(name).source = source;
    }

    public static Sound GetSound(string name)
    {
        if (Instance == null)
        {
            Debug.LogWarning("please create an instance with AudioPlayer before using");
            return null;
        }

        if (!m_Sounds.ContainsKey(name))
        {
            Debug.LogWarning("'" + name + "' does not exist");
            return m_Sounds["error"];
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

    // -- Coroutines --

    private IEnumerator PlayWithDelay(Sound sound, float delay)
    {
        yield return new WaitForSeconds(delay);
        Play(sound.Name);
    }
    private IEnumerator PlayOverlapWithDelay(Sound sound, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayOverlap(sound.Name);
    }
    private IEnumerator PlaySeperatedWithDelay(Sound sound, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySeperated(sound.Name);
    }
    private IEnumerator PlayWithDelayAtPoint(Sound sound, float delay, Vector3 position, float spatialBlend)
    {
        yield return new WaitForSeconds(delay);
        PlayAtPoint(sound.Name, position, spatialBlend);
    }
}
