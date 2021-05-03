using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    public int keysToUnluck;
  
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player" && GameVariables.keysInventory >= keysToUnluck )
        {
            GameVariables.keysInventory -= keysToUnluck;
            Destroy(gameObject);
        }
    }
}
