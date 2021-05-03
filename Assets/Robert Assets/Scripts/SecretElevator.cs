using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretElevator : MonoBehaviour
{
    public GameObject objectToMove;
    
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.gameObject.name == "Player")
        {
            Vector3 objectPosition = objectToMove.transform.position;
            objectPosition.y *= -1;
            objectToMove.transform.position = objectPosition;
        }
    }
}
