
using UnityEngine;

public class Bow : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera thirdPersonCam;

    // Update is called once per frame
    void Update()
    {
      if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }   
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(thirdPersonCam.transform.position, thirdPersonCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}

