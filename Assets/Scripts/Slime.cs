using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameObject   heroKnightGO;
    public GameObject   ballPrefab;
    public Transform    ballStartL;
    public Transform    ballStartR;
    public float        speed;
    public int          life;
    public int          damage;
    public enum SlimeState
    {
        Idle,
        Walking,
        Attacking,
        Dead
    }

    private Animator    slimeAnimator;
    private float       m_ballShoot;
    private float       m_timeSinceDestroy = 0f;
    private bool        m_isDead = false;
    private bool        m_shoot;
    private bool        m_startedAttack = false;
    private int         m_facingDirection = 1;

    // Slime IA Variables
    private SlimeState currentState = SlimeState.Idle;
    private float stateTimer = 0f;
    private Vector2 moveDirection = Vector2.right; // o Vector2.left aleatoriamente

    // Start is called before the first frame update
    void Start()
    {
        slimeAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

        else if (Mathf.Abs(transform.position.x - heroKnightGO.transform.position.x) < 2f &&
                 Mathf.Abs(transform.position.y - heroKnightGO.transform.position.y) < 2f)
        {
            switch (currentState)
            {
                case SlimeState.Idle:
                    HandleIdle();
                    break;
                case SlimeState.Walking:
                    HandleWalking();
                    break;
                case SlimeState.Attacking:
                    if (!m_startedAttack)
                    {
                        m_ballShoot = slimeAnimator.runtimeAnimatorController.animationClips[1].length / 2;
                        m_shoot = false;
                        m_startedAttack = true;
                    }
                    HandleAttacking();
                    break;
            }

            if (GetComponent<SpriteRenderer>().flipX)
                m_facingDirection = -1;
            else
                m_facingDirection = 1;

            if (life <= 0 && !m_isDead)
            {
                m_isDead = true;
                currentState = SlimeState.Dead;
                m_timeSinceDestroy = slimeAnimator.runtimeAnimatorController.animationClips[3].length + 2f;
                //GetComponent<BoxCollider2D>().enabled = false;
                slimeAnimator.SetTrigger("Dead"); ;
            }
        }
        else
        {
            slimeAnimator.SetInteger("AnimState", 0);
        }
    }

    void HandleIdle()
    {
        slimeAnimator.SetInteger("AnimState", 0); // Idle

        stateTimer -= Time.deltaTime;
        m_startedAttack = false;
        if (stateTimer <= 0)
        {
            int nextAction = Random.Range(0, 3); // 0 = idle, 1 = walk, 2 = attack
            if (nextAction == 0)
            {
                stateTimer = Random.Range(1f, 2f);
            }
            else if (nextAction == 1)
            {
                moveDirection = Random.value < 0.5f ? Vector2.left : Vector2.right;
                GetComponent<SpriteRenderer>().flipX = moveDirection == Vector2.left;
                currentState = SlimeState.Walking;
                stateTimer = Random.Range(1f, 3f);
            }
            else
            {
                currentState = SlimeState.Attacking;
                stateTimer = slimeAnimator.runtimeAnimatorController.animationClips[0].length;
            }
        }
    }

    void HandleWalking()
    {
        slimeAnimator.SetInteger("AnimState", 2); // Walk
        transform.Translate(moveDirection * speed * Time.deltaTime);

        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0)
        {
            currentState = SlimeState.Idle;
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
        slimeAnimator.SetInteger("AnimState", 1); // Attack
        stateTimer -= Time.deltaTime;
        m_ballShoot -= Time.deltaTime;

        if (stateTimer <= 0)
        {
            currentState = SlimeState.Idle;
            stateTimer = Random.Range(1f, 2f);
        }

        if (m_ballShoot <= 0 && !m_shoot)
        {
            if (m_facingDirection == 1)
                Instantiate(ballPrefab, ballStartR.position, Quaternion.identity);
            else
                Instantiate(ballPrefab, ballStartL.position, Quaternion.identity);
            m_shoot = true;
        }
    }

    public void TakeDamage (int dmg)
    {
        life -= dmg;
        slimeAnimator.SetTrigger("Hit");
    }

    public bool GetIsDead()
    {
        return m_isDead;
    }
}
