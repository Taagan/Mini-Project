using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss_Dog : Enemy
{
    [SerializeField] public GameObject magicMissle;
    [SerializeField] public GameObject minion;
    [SerializeField] private GameObject sword;
    [SerializeField] private Rigidbody rb;
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


    private void Start()
    {
        navMeshAgent.speed = speed;
        hp = maxHp;
    }


    public void FixedUpdate()
    {
        baseNode.Execute();
        staminaSlider.value = stamina / maxStamina;
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
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
        sword.transform.Rotate(new Vector3(0, 10, 0));
        stamina -= phase_I_StaminaRate;
        //Do spin
    }

    private void Phase_II()
    {
        if (sword.gameObject.active)
        {
            sword.SetActive(false);
        }
        GameObject missle = (GameObject)Instantiate(magicMissle, this.transform.position + (transform.forward * 5), this.transform.rotation);
        missle.GetComponent<MagicMissleScript>().destiantion = player.transform.position;
        navMeshAgent.isStopped = true;
        stamina -= phase_II_StaminaRate;
        //Shot magic missles
    }

    private void Phase_III()
    {

        Vector3 targetDir = player.transform.position - transform.position;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 100, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir);

        Instantiate(minion, this.transform.position + (transform.forward * 5), this.transform.rotation);
        Instantiate(minion, this.transform.position + (transform.forward * 10), this.transform.rotation);
        stamina -= phase_III_StaminaRate;
        //Spawn minions
    }

    private void Phase_IV()
    {
        if (charging)
        {
            transform.position += transform.forward * speed * Time.deltaTime * 3;
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

    public override void Idle()
    {
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
                stamina -= phase_IV_StaminaRate;
                rb.velocity = Vector3.zero;
                charging = false;
            }
        }
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