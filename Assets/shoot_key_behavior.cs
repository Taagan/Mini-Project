using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_key_behavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Arrow"))
        {
            Destroy(gameObject);

            GameVariables.keysInventory++;
        }
    }
}
