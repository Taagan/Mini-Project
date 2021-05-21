using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class normalDogScript : MonoBehaviour
{

    Animator animator;
    NavMeshAgent agent;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.steeringTarget != Vector3.zero)
            animator.SetBool("moving", true);
        else
            animator.SetBool("moving", false);

        //if (rb.velocity != Vector3.zero)
        //    animator.SetBool("moving", true);
        //else
        //    animator.SetBool("moving", false);

    }
}
