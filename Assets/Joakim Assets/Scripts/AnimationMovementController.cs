using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationMovementController : MonoBehaviour
{
    private Animator animator;
    private MovementInputController movement;

    private Vector2 smoothDeltaPos = Vector2.zero;
    public Vector2 velocity = Vector2.zero;
    public float magnitude = 0.25f;
    private void OnEnable()
    {
        arrowSound = GetComponent<AudioSource>();
        movement = GetComponent<MovementInputController>();
        animator = GetComponent<Animator>();
    }

    public AudioSource arrowSound;

    public bool moving;
    public bool shouldTurn;
    public float turn;

    public GameObject look;

    public GameObject arrow;

    public Transform arrowBone;
    public GameObject arrowPrefab;

 
   
    public void Update()
    {
        Vector3 worldPosition = movement.nextPosition - transform.position;

        //Map to local space
        float x = Vector3.Dot(transform.right, worldPosition);
        float y = Vector3.Dot(transform.forward, worldPosition);
        Vector2 deltaPosition = new Vector2(x, y);

        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPos = Vector2.Lerp(smoothDeltaPos, deltaPosition, smooth);
      
        if (Time.deltaTime > 1e-5f)
        {
            velocity = smoothDeltaPos / Time.deltaTime;
        }

       
        moving = velocity.magnitude > magnitude;

        bool isAiming = (movement.aimInput == 1f);


        if (isAiming)
        {
            if ((movement.fireInput == 1f))
            {
                animator.SetTrigger("Fire");
                arrow.SetActive(false);
                StartCoroutine(FireArrow());
            }


            if (animator.GetCurrentAnimatorStateInfo(2).IsName("Fire"))
            {
                arrow.SetActive(false);
            }
            else
            {
                arrow.SetActive(true);
            }

            movement.fireInput = 0f;
        }

        animator.SetBool("IsAiming", isAiming);
        animator.SetBool("IsMoving", moving);
        animator.SetFloat("VelocityX", velocity.x);
        animator.SetFloat("VelocityY", Mathf.Abs(velocity.y));  
    }

    private void OnAnimatorMove()
    {
        transform.position = movement.nextPosition;
    }

    [SerializeField]
    private Transform fireTransform;

    IEnumerator FireArrow()
    {
        GameObject projectile = Instantiate(arrowPrefab);
        projectile.transform.forward = look.transform.forward;
        projectile.transform.position = fireTransform.position + fireTransform.forward;
        yield return new WaitForSeconds(0.1f);
        arrowSound.Play();
        projectile.GetComponent<ArrowProjectile>().Fire();

    }


}
