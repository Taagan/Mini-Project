using UnityEngine;

public class CheckpointCollision : MonoBehaviour
{
    [SerializeField, Tooltip("Spawn Position relative to transform")]
    private Vector3 m_SpawnPosition = Vector3.up;

    private ParticleSystem m_Particles = null;

    /// <summary>
    /// object that is used to modify when loading checkpoint, e.g. Player
    /// </summary>
    public GameObject SavedObject { get; private set; } = null;
    public bool IsFlagged { get; set; } = false;

    private void Awake()
    {
        m_Particles = transform.parent.GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsFlagged)
            return;

        if (other.CompareTag("Player"))
        {
            SavedObject = other.gameObject;

            CheckpointManager.SaveCheckpoint(this,
                gameObject.transform.position + m_SpawnPosition,
                new Vector3(0.0f, gameObject.transform.rotation.eulerAngles.y, 0.0f));
            AudioPlayer.PlayOverlap("checkpoint");
        }
    }

    /// <summary>
    /// depending on if this is current checkpoint or not, set suitable color to particles
    /// </summary>
    public void SetColor()
    {
        ParticleSystem.MainModule main = m_Particles.main;
        main.startColor = (IsFlagged) ? Color.blue : Color.red;
    }
}
