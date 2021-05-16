using UnityEngine;

public class CheckpointCollision : MonoBehaviour
{
    [SerializeField]
    private bool m_CustomSpawnPosition = true;
    [SerializeField, Tooltip("Spawn Position relative to transform")]
    private Vector3 m_SpawnPosition = Vector3.up;

    private ParticleSystem m_Particles = null;
    private bool m_IsFlagged;

    /// <summary>
    /// object that is used to modify when loading checkpoint, e.g. Player
    /// </summary>
    public GameObject SavedObject { get; private set; } = null;
    public bool IsFlagged
    {
        get { return m_IsFlagged; }
        set 
        {
            m_IsFlagged = value;
            SetColor();
        } 
    }

    private void Awake()
    {
        m_Particles = transform.GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsFlagged)
            return;

        if (other.CompareTag("Player"))
        {
            SavedObject = other.gameObject;

            Vector3 spawnPos = (m_CustomSpawnPosition) ? m_SpawnPosition : new Vector3(0, other.bounds.extents.y, 0);

            CheckpointManager.SaveCheckpoint(this,
                gameObject.transform.position + spawnPos,
                new Vector3(0.0f, gameObject.transform.rotation.eulerAngles.y, 0.0f));
            AudioPlayer.PlayOverlap("checkpoint");
        }
    }

    /// <summary>
    /// depending on if this is current checkpoint or not, set suitable color to particles
    /// </summary>
    private void SetColor()
    {
        ParticleSystem.MainModule main = m_Particles.main;
        main.startColor = (IsFlagged) ? Color.blue : Color.red;
    }
}
