using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [HideInInspector] public AudioSource source;

    [SerializeField] private string m_Name = string.Empty;

    [SerializeField] private AudioClip m_Clip = null;
    [SerializeField] private AudioMixerGroup m_Output = null;

    [SerializeField, Range(0.0f, 1.0f)] public float m_Volume = 1.0f;
    [SerializeField, Range(0.1f, 3.0f)] public float m_Pitch = 1.0f;

    [SerializeField] private bool m_Loop = false;

    private string m_Id = System.Guid.NewGuid().ToString();

    public string ID => m_Id;
    public string Name => m_Name;

    public AudioClip Clip => m_Clip;
    public AudioMixerGroup Output => m_Output;

    public float Volume => m_Volume;
    public float Pitch => m_Pitch;

    public bool Loop => m_Loop;
}
