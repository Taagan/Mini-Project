using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public MovementInputController inputController;

    public GameObject mainCamera;
    public GameObject aimCamera;
    public GameObject aimReticle;

    // Start is called before the first frame update
    void Start()
    {
        inputController = GetComponent<MovementInputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputController.aimInput == 1f && !aimCamera.activeInHierarchy)
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);

            StartCoroutine(ShowReticle());
        }
        else if (inputController.aimInput != 1f && !mainCamera.activeInHierarchy)
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            aimReticle.SetActive(false);
        }
    }

    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.25f);
        aimReticle.SetActive(enabled);
    }

}
