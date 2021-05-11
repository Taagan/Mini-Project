using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Chicken : Enemy
{
    [SerializeField] public Animator animator;
    [SerializeField] public GameObject minion;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float restingAmountStamina = 100;
    [SerializeField] private float restingRateStamina = 0.4f;
    [SerializeField] private float maxStamina;
    [SerializeField] private float idleRateStamina = 0.4f;
    [SerializeField] private float phase_I_StaminaRate = 25f;
    [SerializeField] private float phase_II_StaminaRate = 50f;

    private bool charging;
    private bool phaseChange;

    private Vector3 lastPlayerPos;

    Node baseNode;


    public Boss_Chicken()
    {
        baseNode = new SelectorNode(this);

        SequenceNode activeSeq = new SequenceNode(this);
        SequenceNode staminaChargeCheckSeq = new SequenceNode(this);
        IdleNode idle = new IdleNode(this);

        PlayerInArena playerInArena = new PlayerInArena(this);
        Stamina staminaCheck = new Stamina(this);
        SelectorNode phaseSelector = new SelectorNode(this);

        PlayerInArena playerInChickenArena = new PlayerInArena(this);
        Stamina staminaCheckChicken = new Stamina(this);

        SpawnMinions spawnMinions = new SpawnMinions(this);
        Boss_Chicken_Charge bossChickenCharge = new Boss_Chicken_Charge(this);

        baseNode.AddChild(activeSeq);
        baseNode.AddChild(idle);


        activeSeq.AddChild(playerInChickenArena);
        activeSeq.AddChild(phaseSelector);

        phaseSelector.AddChild(staminaChargeCheckSeq);
        phaseSelector.AddChild(spawnMinions);

        staminaChargeCheckSeq.AddChild(staminaCheckChicken);
        staminaChargeCheckSeq.AddChild(bossChickenCharge);


    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.speed = speed;
        hp = maxHp;
        animator = GetComponentInChildren<Animator>();
    }

    public void FixedUpdate()
    {
        baseNode.Execute();
    }

    public override void Attack()
    {
        switch (currentPhase)
        {
            case Phase.I:
                Phase_I();
                break;
            case Phase.II:
                Phase_II();
                break;
            default:
                break;
        }
    }

    private void Phase_I()
    {
        if (phaseChange)
        {
            phaseChange = false;
        }
        else
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Phase1", true);
            animator.SetBool("Phase2", false);
            if (charging)
            {
                transform.position += transform.forward * speed * Time.deltaTime * 3;
                animator.SetBool("Charging", true);
            }
            else if (!charging)
            {
                lastPlayerPos = player.transform.position;

                Vector3 targetDir = lastPlayerPos - transform.position;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1000, 0.0f);

                transform.rotation = Quaternion.LookRotation(newDir);

                charging = true;
            }
            //Charge
        }
        
    }

    private void Phase_II()
    {
        LookAtPlayer();
        if (!phaseChange)
        {
            animator.SetTrigger("PhaseChange");
            phaseChange = true;
        }
        else
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Phase1", false);
            animator.SetBool("Phase2", true);
            Vector3 targetDir = player.transform.position - transform.position;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 100, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDir);

            Instantiate(minion, this.transform.position + (transform.forward * 5), this.transform.rotation);
            Instantiate(minion, this.transform.position + (transform.forward * 10), this.transform.rotation);
            animator.SetTrigger("Attack");
            stamina -= phase_II_StaminaRate;
            //Spawn minions
        }
    }


    public override void Idle()
    {
        LookAtPlayer();
        animator.SetBool("Idle", true);
        rb.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;
        if (resting)
        {
            stamina += restingRateStamina;
            if (stamina >= restingAmountStamina || stamina >= maxStamina)
            {
                resting = false;
            }
        }
        else
        {
            stamina += idleRateStamina;
        }
        if (stamina >= maxStamina)
        {
            stamina = maxStamina;
        }
        else if (stamina <= 0)
        {
            stamina = 0;
        }
        //Do idle behaviour
        Debug.Log("Idle");

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (charging)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
            {
                stamina -= phase_I_StaminaRate;
                rb.velocity = Vector3.zero;
                animator.SetTrigger("Dizzy");
                charging = false;
            }
        }
    }
}
