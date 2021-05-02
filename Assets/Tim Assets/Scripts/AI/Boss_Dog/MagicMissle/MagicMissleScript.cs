using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissleScript : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifetime;
    public Vector3 destiantion;


    private void Start()
    {
        Vector3 targetDir = destiantion - transform.position;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1000, 0.0f);

        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);

        Destroy(gameObject, lifetime);
    }


    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Do damage
        }

        Destroy(gameObject);
    }
}
