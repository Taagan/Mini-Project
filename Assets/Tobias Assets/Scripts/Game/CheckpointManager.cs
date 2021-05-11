using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// manages save and load of checkpoints 
/// </summary>
public class CheckpointManager : MonoBehaviour
{
    public static CheckpointCollision Checkpoint { get; private set; } = null;

    public static Vector3 Position { get; set; }
    public static Vector3 Rotation { get; set; }


    public static bool SaveCheckpoint(CheckpointCollision cpColl, Vector3 position, Vector3 rotation)
    {
        if (Checkpoint != null &&
            Checkpoint == cpColl) return false;

        Position = position;
        Rotation = rotation;

        if (Checkpoint != null)
        {
            Checkpoint.IsFlagged = false;
            Checkpoint.SetColor();
        }

        cpColl.IsFlagged = true;
        cpColl.SetColor();

        Checkpoint = cpColl;

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

        CharacterController charCtrl = null;

        // deactivate character controller temporarily to modify object's position
        if (charCtrl = obj.GetComponent<CharacterController>())
        {
            charCtrl.enabled = false;
        }

        obj.transform.position = Position;

        if (charCtrl)
        {
            charCtrl.enabled = true;
        }

        obj.transform.rotation = Quaternion.Euler(Rotation);

        return true;
    }
}
