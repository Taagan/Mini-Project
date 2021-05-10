using UnityEngine;


public class AnimationMovementController : MonoBehaviour
{
    private Animator animator;
    private MovementInputController movement;

    private Vector2 smoothDeltaPos = Vector2.zero;
    public Vector2 velocity = Vector2.zero;
    public float magnitude = 0.25f;
    public bool moving;

    private void OnEnable()
    {
        movement = GetComponent<MovementInputController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 worldPosition = movement.nextPosition - transform.position;

        float x = Vector3.Dot(transform.right, worldPosition);
        float y = Vector3.Dot(transform.forward, worldPosition);
        Vector2 deltaPosition = new Vector2(x, y);

        float smoothing = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPos = Vector2.Lerp(smoothDeltaPos, deltaPosition, smoothing);

        if (Time.deltaTime > 1e-5f)
        {
            velocity = smoothDeltaPos / Time.deltaTime;
        }

        moving = velocity.magnitude > magnitude;

        bool isAiming = (movement.aimInput == 1f);
        bool fire = (movement.fireInput == 1f);

        if (isAiming)
        {
            if (fire)
            {
                animator.SetTrigger("Fire");
                //Skjut pil-metod
            }
            if (animator.GetCurrentAnimatorStateInfo(2).IsName("Fire"))
            {
                //Avaktivera någon form av projektil
            }
            else
            {
                //aktivera pil
            }
        }

        animator.SetBool("Aiming", isAiming);
        animator.SetBool("Moving", moving);
        animator.SetFloat("VelocityX", velocity.x);
        animator.SetFloat("VelocityY", Mathf.Abs(velocity.y));
    }

    private void OnAnimatorMove()
    {
        transform.position = movement.nextPosition;
    }


}
