using UnityEngine;

/// <summary>
/// manages save and load for checkpoints 
/// </summary>
public class CheckpointManager : MonoBehaviour
{
    public static CheckpointCollision Checkpoint { get; private set; } = null;

    public static Vector3 Position { get; set; }
    public static Vector3 Rotation { get; set; }

    public static bool SaveCheckpoint(CheckpointCollision checkpoint, Vector3 position, Vector3 rotation)
    {
        if (Checkpoint != null &&
            Checkpoint == checkpoint) return false;

        Position = position;
        Rotation = rotation;

        if (Checkpoint != null)
            Checkpoint.IsFlagged = false;

        checkpoint.IsFlagged = true;

        Checkpoint = checkpoint;

        return true;
    }

    /// <summary>
    /// set object to position and rotation
    /// </summary>
    public static bool LoadCheckpoint()
    {
        if (Checkpoint == null || Checkpoint.SavedObject == null)
            return false;

        GameObject obj = Checkpoint.SavedObject;

        CharacterController charCtrl = obj.GetComponent<CharacterController>();

        if (charCtrl != null) // deactivate character controller temporarily to modify object's position
            charCtrl.enabled = false;

        obj.transform.position = Position;

        if (charCtrl != null)
            charCtrl.enabled = true;

        obj.transform.rotation = Quaternion.Euler(Rotation);

        if (obj.CompareTag("Player"))
        {
            obj.GetComponent<PlayerHealth>().ResetPlayer();
            obj.GetComponent<MovementInputController>().nextPosition = Position; // set new position...
            obj.GetComponent<AnimationMovementController>().getHit(); // ugly fix to prevent player being stuck in dead animation
        }

        return true;
    }
}
