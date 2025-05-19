using UnityEngine;
using System.Collections;
using static Slime;
using UnityEngine.EventSystems;

public class Bandit : MonoBehaviour {

    [SerializeField] float      m_jumpForce = 7.5f;

    public GameObject   heroKnightGO;
    public int          life;
    public float        speed;
    public float        damage;

    public enum BanditState
    {
        Idle,
        Walking,
        Jumping,
        Attacking,
        Dead
    }

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private int                 m_facingDirection;
    private bool                m_grounded = false;
    private bool                m_combatIdle = false;
    private bool                m_startedAttack = false;
    private bool                m_isDead = false;
    private float               m_timeSinceDestroy = 0f;

    // Bandit IA Variables
    private BanditState currentState = BanditState.Idle;
    private float stateTimer = 0f;
    private Vector2 moveDirection = Vector2.right; // o Vector2.left aleatoriamente

    // Use this for initialization
    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        m_facingDirection = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_isDead)
        {
            if (m_timeSinceDestroy > 0f)
                m_timeSinceDestroy -= Time.deltaTime;
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        else if (Mathf.Abs(transform.position.x - heroKnightGO.transform.position.x) < 20f &&
                 Mathf.Abs(transform.position.y - heroKnightGO.transform.position.y) < 20f)
        {
            switch (currentState)
            {
                case BanditState.Idle:
                    HandleIdle();
                    break;
                case BanditState.Walking:
                    HandleWalking();
                    break;
                case BanditState.Attacking:
                    HandleAttacking();
                    break;
                case BanditState.Jumping:
                    HandleJump();
                    break;
            }

            m_combatIdle = true;
            m_animator.SetBool("CombatIdle", true);

            if (m_body2d.velocity.x < 0)
                m_facingDirection = -1;
            else if (m_body2d.velocity.x > 0)
                m_facingDirection = 1;

                if (GetComponent<SpriteRenderer>().flipX)
                m_facingDirection = -1;
            else
                m_facingDirection = 1;

            //Check if character just landed on the ground
            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //Check if character just started falling
            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            // Swap direction of sprite depending on walk direction
            if (m_facingDirection == 1)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (m_facingDirection == -1)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

            // -- Handle Animations --
            //Death
            if (life <= 0 && !m_isDead)
            {
                m_animator.SetTrigger("Death");
                m_isDead = true;
            }
        }
        else
        {
            m_combatIdle = false;
            m_animator.SetBool("CombatIdle", false);
            m_animator.SetInteger("AnimState", 0);
        }
    }

    void HandleIdle()
    {
        m_animator.SetInteger("AnimState", 1); // Idle

        stateTimer -= Time.deltaTime;
        m_startedAttack = false;

        if (stateTimer <= 0)
        {
            int nextAction = Random.Range(0, 4); // 0 = idle, 1 = walk, 2 = attack, 3 = jump
            if (nextAction == 0)
            {
                stateTimer = Random.Range(1f, 3f);
            }
            else if (nextAction == 1)
            {
                moveDirection = Random.value < 0.5f ? Vector2.left : Vector2.right;
                GetComponent<SpriteRenderer>().flipX = moveDirection == Vector2.left;
                currentState = BanditState.Walking;
                stateTimer = Random.Range(1f, 2f);
            }
            else if (nextAction == 2)
            {
                currentState = BanditState.Attacking;
                stateTimer = m_animator.runtimeAnimatorController.animationClips[0].length;
            }
            else if (nextAction == 3)
            {
                currentState = BanditState.Jumping;
                stateTimer = 1.0f; // Tiempo de salto
            }
        }
    }

    void HandleWalking()
    {
        m_animator.SetInteger("AnimState", 2); // Walk
        transform.Translate(moveDirection * speed * Time.deltaTime);

        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0)
        {
            currentState = BanditState.Idle;
            stateTimer = Random.Range(1f, 3f);
            m_startedAttack = false;
        }
    }

    void HandleAttacking()
    {
        if (transform.position.x > heroKnightGO.transform.position.x)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
        m_animator.SetInteger("AnimState", 1); // Attack
        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0)
        {
            currentState = BanditState.Idle;
            stateTimer = Random.Range(1f, 2f);
        }
    }

    void HandleJump()
    {
        if (m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_grounded = false;
            m_animator.SetBool("Grounded", false);
        }

        stateTimer -= Time.deltaTime;

        // Vuelve al estado Idle después de un tiempo
        if (stateTimer <= 0)
        {
            currentState = BanditState.Idle;
            stateTimer = Random.Range(1f, 2f);
        }
    }

    public void TakeDamage(int dmg)
    {
        life -= dmg;
        m_animator.SetTrigger("Hit");
    }

    public bool GetIsDead()
    {
        return m_isDead;
    }
}
