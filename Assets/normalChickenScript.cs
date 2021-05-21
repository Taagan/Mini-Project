using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class normalChickenScript : MonoBehaviour
{

    NavMeshAgent agent;
    Animator animator;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        if (distance > 1f)
        {
            animator.SetBool("walk", true);
            animator.SetBool("eat", false);
        }
        else
        {
            animator.SetBool("walk", false);
            animator.SetBool("eat", true);
        }
    }
}
