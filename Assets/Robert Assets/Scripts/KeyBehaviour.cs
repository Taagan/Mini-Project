using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("Arrow"))
        {
            GameVariables.keysInventory += 1;
            Destroy(gameObject);
            AudioPlayer.PlayAtPoint("key-pickup", gameObject.transform.position, 0.25f);
        }
    }
}
