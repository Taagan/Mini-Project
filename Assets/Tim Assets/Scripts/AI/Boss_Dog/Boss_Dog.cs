using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss_Dog : Enemy
{
    [SerializeField] public GameObject magicMissle;
    [SerializeField] public GameObject minion;
    [SerializeField] private GameObject sword;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Animator animator;
    [SerializeField] private float maxStamina;
    [SerializeField] private float restingAmountStamina = 100;
    [SerializeField] private float restingRateStamina = 0.4f;
    [SerializeField] private float phase_I_StaminaRate = 0.5f;
    [SerializeField] private float phase_II_StaminaRate = 100f;
    [SerializeField] private float phase_III_StaminaRate = 25f;
    [SerializeField] private float phase_IV_StaminaRate = 25f;

    [SerializeField] private float idleRateStamina = 0.4f;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private NavMeshAgent navMeshAgent;
    private bool charging;
    private bool phaseChange;
    private bool phaseChange2;
    private bool phaseChange3;

    [SerializeField]private WinMenu wm;
    private Vector3 lastPlayerPos;

    Node baseNode;

    public Boss_Dog()
    {
        baseNode = new SelectorNode(this);

        SequenceNode active = new SequenceNode(this);
        IdleNode idle = new IdleNode(this);

        PlayerInArena playerInArena = new PlayerInArena(this);
        Stamina staminaCheck = new Stamina(this);
        SelectorNode phaseSelector = new SelectorNode(this);

        Phase_I phase_I = new Phase_I(this);
        Phase_II phase_II = new Phase_II(this);
        Phase_III phase_III = new Phase_III(this);
        Phase_IV phase_IV = new Phase_IV(this);

        baseNode.AddChild(active);
        baseNode.AddChild(idle);

        active.AddChild(playerInArena);
        active.AddChild(staminaCheck);
        active.AddChild(phaseSelector);

        phaseSelector.AddChild(phase_I);
        phaseSelector.AddChild(phase_II);
        phaseSelector.AddChild(phase_III);
        phaseSelector.AddChild(phase_IV);
    }


    protected override void Start()
    {
        base.Start();
        navMeshAgent.speed = speed;
        hp = maxHp;
        animator = GetComponentInChildren<Animator>();
    }


    public void FixedUpdate()
    {
        baseNode.Execute();
        staminaSlider.value = (float)hp / maxHp;
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
            case Phase.III:
                Phase_III();
                break;
            case Phase.IV:
                Phase_IV();
                break;
            default:
                break;
        }
    }


    private void Phase_I()
    {
        Debug.Log("in phase 1");
        animator.SetBool("Phase1", true);
        animator.SetBool("Idle", false);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
        sword.transform.Rotate(new Vector3(0, 10, 0));
        stamina -= phase_I_StaminaRate;
        //Do spin
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
            if (sword.gameObject.active)
            {
                sword.SetActive(false);
            }
            animator.SetBool("Idle", false);
            animator.SetBool("Phase1", false);
            animator.SetBool("Phase2", true);

            GameObject missle = (GameObject)Instantiate(magicMissle, this.transform.position + (transform.forward * 5), this.transform.rotation);
            missle.GetComponent<MagicMissleScript>().destiantion = player.transform.position;
            animator.SetTrigger("Attack");
            navMeshAgent.isStopped = true;
            stamina -= phase_II_StaminaRate;
            //Shot magic missles
        }
    }

    private void Phase_III()
    {
        LookAtPlayer();
        if (!phaseChange2)
        {
            animator.SetTrigger("PhaseChange");
            phaseChange2 = true;
        }
        else
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Phase1", false);
            animator.SetBool("Phase2", false);
            animator.SetBool("Phase3", true);
            Vector3 targetDir = player.transform.position - transform.position;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 100, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDir);

            Instantiate(minion, this.transform.position + (transform.forward * 5), this.transform.rotation);
            Instantiate(minion, this.transform.position + (transform.forward * 10), this.transform.rotation);
            animator.SetTrigger("Attack");
            stamina -= phase_III_StaminaRate;
            //Spawn minions
        }
    }

    private void Phase_IV()
    {
        if (!phaseChange3)
        {
            animator.SetTrigger("PhaseChange");
            phaseChange3 = true;
        }
        else
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Phase1", false);
            animator.SetBool("Phase2", false);
            animator.SetBool("Phase3", false);
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

    private void OnTriggerEnter(Collider collision)
    {
        if (charging)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
            {
                stamina -= phase_IV_StaminaRate;
                rb.velocity = Vector3.zero;
                animator.SetTrigger("Dizzy");
                charging = false;
            }
        }
        if (collision.gameObject.tag == "Arrow")
        {
            TakeDamage(2);
            if (hp <= 0)
            {
                wm.Notify();
                Destroy(navMeshAgent);
                Destroy(staminaSlider);
                animator.SetTrigger("DIE");
            }
            Destroy(collision.gameObject);
        }
    }

    



    private void LookAtPlayer()
    {
        Vector3 targetDir = player.transform.position - transform.position;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1000, 0.0f);

        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);
    }


    [SerializeField]
    public void UpdateStats(GameObject player, int maxHp, int speed, int atkDamage, int aggroRange, int stamina, float courage)
    {
        this.player = player;
        this.maxHp = maxHp;
        this.speed = speed;
        this.atkDamage = atkDamage;
        this.aggroRange = aggroRange;
        this.stamina = stamina;
        this.courage = courage;
    }
}