using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player" || collider.gameObject.name == "Arrow")
        {
            GameVariables.keysInventory += 1;
            Destroy(gameObject);
        }
    }
}
