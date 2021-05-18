using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBehaviour : MonoBehaviour
{
    public GameObject hiddenSecret;
    public bool secretKeeper;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Arrow"))
        {
            GameVariables.crateDestroys++;
            Destroy(gameObject);

            if (secretKeeper)
            {
                Instantiate(hiddenSecret, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
