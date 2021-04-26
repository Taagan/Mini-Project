using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player" && GameVariables.keyCount >= 3 )
        {
            GameVariables.keyCount = 0;
            Destroy(gameObject);
        }
    }
}
