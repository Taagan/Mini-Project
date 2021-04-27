using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBehaviour : MonoBehaviour
{
    public GameObject RevealKey;
    public Vector3 RevealKeyPosition = new Vector3(-4, 1, 3);

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player") // TODO: Change to name == Arrow
        {
            GameVariables.crateCount++;
            Destroy(gameObject);
            if (GameVariables.crateCount >= 4)
            {
                Instantiate(RevealKey, RevealKeyPosition, Quaternion.identity);
            }
        }
    }
}
