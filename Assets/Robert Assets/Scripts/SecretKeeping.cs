using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretKeeping : MonoBehaviour
{
    public GameObject hiddenSecret;
    public bool secretKeeper;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Arrow"))
        {
            GameVariables.enemyKills++;
            Destroy(gameObject);

            if (GameVariables.enemyKills % 5 == 0)
            {
                Instantiate(hiddenSecret, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
