using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using System.Drawing;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed;
    [SerializeField] float      m_jumpForce;
    [SerializeField] float      m_rollForce;
    [SerializeField] bool       m_noBlood;
    [SerializeField] GameObject m_slideDust;

    public GameObject           hkHitboxGO;
    public GameObject           shieldGO;
    public GameObject           swordHitbox;
    public GameObject           fireballPrefab;
    public GameObject           fireballStartR;
    public GameObject           fireballStartL;

    private GameObject          gameManager;
    private GameObject          cameraGO;
    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor1;
    private Sensor_HeroKnight   m_groundSensor2;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    private bool                m_isWallSliding = false;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private bool                m_doubleJump = false;
    private bool                m_isCasting = false;
    private bool                m_isDead = false;
    private int                 m_lifePoints;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               inputX;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_timeSinceCast = 0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;
    private float               m_auxBlock;
    private float               m_attackOnAir = 0.0f;
    public AudioSource          swordSound;
    public AudioSource          jumpGrunt;
    public AudioSource          slidingSound;
    public AudioSource          landingSound;
    public AudioSource          hurtSound;
    public AudioSource          shieldSound;
    //private bool landingPlayed = false;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager");
        inputX = Input.GetAxis("Horizontal");
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor1 = transform.Find("GroundSensor1").GetComponent<Sensor_HeroKnight>();
        m_groundSensor2 = transform.Find("GroundSensor2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        m_auxBlock = m_speed;
        gameManager.GetComponent<GUIController>().ResetLife();
        //m_rollDuration = m_animator.GetCurrentAnimatorStateInfo("Roll");
    }

    // Update is called once per frame
    void Update ()
    {
        m_lifePoints = gameManager.GetComponent<GUIController>().GetLifePoints();

        if (!m_isDead)
        {
            // Increase timer that controls attack combo
            m_timeSinceAttack += Time.deltaTime;

            // Increase timer that checks roll duration
            if (m_rolling)
                m_rollCurrentTime += Time.deltaTime;

            // Disable rolling if timer extends duration
            if (m_rollCurrentTime > m_rollDuration)
                m_rolling = false;

            //Check if character just landed on the ground
            if (!m_grounded && (m_groundSensor1.State() || m_groundSensor2.State()))
            {
                m_grounded = true;
                m_doubleJump = true;
                m_attackOnAir = 0f;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //Check if character just started falling
            if (m_grounded && !m_groundSensor1.State() && !m_groundSensor2.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            if (m_timeSinceCast <= 0)
                m_isCasting = false;
            else
                m_timeSinceCast -= Time.deltaTime;

                // -- Handle input and movement --

                if (m_attackOnAir <= 0f && !m_isCasting)
                inputX = Input.GetAxis("Horizontal");

            // Swap direction of sprite depending on walk direction
            if (inputX > 0)// && !m_isWallSliding)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                m_facingDirection = 1;
            }

            else if (inputX < 0)// && !m_isWallSliding)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                m_facingDirection = -1;
            }
            
            // Move
            if (!m_rolling && !m_isCasting)
                m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

            // -- Handle Animations --
            /*Wall Slide
            bool wallRight = m_wallSensorR1.State() && m_wallSensorR2.State();
            bool wallLeft = m_wallSensorL1.State() && m_wallSensorL2.State();

            m_isWallSliding = (wallRight && inputX == 1) || (wallLeft && inputX == -1);
            m_animator.SetBool("WallSlide", m_isWallSliding);

            if (m_isWallSliding && m_body2d.velocity.y < 0f)
                GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            else
                GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            */
            //Death
            if (m_lifePoints <= 0)
            {
                m_animator.SetBool("noBlood", m_noBlood);
                m_animator.SetTrigger("Death");
                m_isDead = true;
            }


            /*Hurt
            else if (Input.GetKeyDown(KeyCode.Q) && !m_rolling)
            {
                m_animator.SetTrigger("Hurt");
                TakeDamage(20);
            }*/

            //Attack
            else if (Input.GetKeyDown(KeyCode.X) && m_timeSinceAttack > 0.25f && !m_rolling && m_speed != 0 && !m_isCasting)
            //&& ((inputX <= 0.5 && inputX >= -0.5) || !m_grounded))
            {
                m_currentAttack++;
                if (!m_grounded) m_attackOnAir = 1f;

                // Loop back to one after third attack
                if (m_currentAttack > 3)
                    m_currentAttack = 1;

                // Reset Attack combo if time since last attack is too large
                if (m_timeSinceAttack > 1f)
                    m_currentAttack = 1;

                // Call one of three attack animations "Attack1", "Attack2", "Attack3"
                m_animator.SetTrigger("Attack" + m_currentAttack);
                // Reproducir el sonido de ataque
                if (swordSound != null)
                {
                    swordSound.Play();  // Reproducir el sonido de ataque
                }
                // Reset timer
                m_timeSinceAttack = 0.0f;
            }

            // Block
            if (Input.GetKeyDown(KeyCode.LeftShift) && !m_rolling && m_grounded && !m_isCasting
                && gameManager.GetComponent<GameManager>().GetShieldAviable())
            {
                shieldGO.SetActive(true);
                hkHitboxGO.GetComponent<CapsuleCollider2D>().size = new Vector2(0.3f * m_facingDirection, 1.25f);
                hkHitboxGO.GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.15f * m_facingDirection, 0.68f);
                m_auxBlock = m_speed;
                m_speed = 0;
                // Reproducir el sonido de shield
                if (shieldSound != null)
                {
                    shieldSound.Play();  // Reproducir el sonido de shield
                }
                m_animator.SetTrigger("Block");
                m_animator.SetBool("IdleBlock", true);
            }

            else if (Input.GetKey(KeyCode.LeftShift) && !m_rolling && m_grounded && !m_isCasting
                    && gameManager.GetComponent<GameManager>().GetShieldAviable())
            {
                shieldGO.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.2f * m_facingDirection, 0.8f);
            }

            else if (Input.GetKeyUp(KeyCode.LeftShift) && !m_rolling && m_grounded && !m_isCasting
                    && gameManager.GetComponent<GameManager>().GetShieldAviable())
            {
                shieldGO.SetActive(false);
                hkHitboxGO.GetComponent<CapsuleCollider2D>().size = new Vector2(0.7133434f * m_facingDirection, 1.25f);
                hkHitboxGO.GetComponent<CapsuleCollider2D>().offset = new Vector2(0f * m_facingDirection, 0.68f);
                m_speed = m_auxBlock;
                m_animator.SetBool("IdleBlock", false);
            }

            //Fireball
            else if (Input.GetKeyDown(KeyCode.A) && !m_rolling && m_grounded && !m_isCasting && gameManager.GetComponent<GameManager>().GetFireballAviable())
            {
                m_timeSinceCast = 1.8f;
                m_isCasting = true;
                m_animator.SetTrigger("Fireball");
            }

            // Roll
            else if (Input.GetKeyDown(KeyCode.C) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                && m_grounded && !m_rolling && !m_isWallSliding && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2
                && gameManager.GetComponent<GameManager>().GetRollAviable())
            {
                m_speed = m_auxBlock;
                m_rolling = true;
                m_rollCurrentTime = 0f;
                if (slidingSound != null)
                {
                    slidingSound.Play();  // Reproducir el sonido de salto
                }
                m_animator.SetTrigger("Roll");
                m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
            }

            //Jump
            else if (Input.GetKeyDown(KeyCode.Z) && m_grounded && !m_rolling && m_speed != 0 && !m_isCasting)
            {
                DisableSwordHitbox();
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                if (jumpGrunt != null)
                {
                    jumpGrunt.Play();  // Reproducir el sonido de salto
                }
                m_groundSensor1.Disable(0.2f);
                m_groundSensor2.Disable(0.2f);
            }

            // Double Jump
            else if (Input.GetKeyDown(KeyCode.Z) && !m_grounded && m_doubleJump && !m_isCasting
                    && gameManager.GetComponent<GameManager>().GetDoubleJumpAviable())
            {
                DisableSwordHitbox();
                m_doubleJump = false;
                m_grounded = false;
                m_animator.SetTrigger("Jump");
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_animator.SetBool("Grounded", m_grounded);
                if (jumpGrunt != null)
                {
                    jumpGrunt.Play();  // Reproducir el sonido de salto
                }
            }

            //Run
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) && !m_isCasting) //Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                m_delayToIdle = 0.05f;
                m_animator.SetInteger("AnimState", 1);
            }

            //Idle
            else
            {
                // Prevents flickering transitions to idle
                m_delayToIdle -= Time.deltaTime;
                if (m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
            }
        }
    }

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }

    public void TakeDamage(int dmg)
    {
        DisableSwordHitbox();
        if (hurtSound != null)
        {
            hurtSound.Play();  // Reproducir el sonido de ataque
        }
        gameManager.GetComponent<GUIController>().UpdateLife(dmg);
        m_animator.SetTrigger("Hurt");
    }

    public int GetFacingDirection()
    {
        return m_facingDirection;
    }

    public bool GetIsDead()
    {
        return m_isDead;
    }
    public void AtackHitbox1()
    {
        swordHitbox.GetComponent<SwordHitbox>().EnableHitbox();
        CapsuleCollider2D box = swordHitbox.GetComponent<CapsuleCollider2D>();
        //swordHitbox.transform.position = new Vector2(transform.position.x, transform.position.y);
        box.size = new Vector2(1.5f,1.1f);
        box.offset = new Vector2(0.75f * m_facingDirection, 0.9f);
    }

    public void AttackHitbox2(int frame)
    {
        swordHitbox.GetComponent<SwordHitbox>().EnableHitbox();
        CapsuleCollider2D box = swordHitbox.GetComponent<CapsuleCollider2D>();
        if (frame == 1)
        {
            //swordHitbox.transform.position = new Vector2(transform.position.x, transform.position.y);
            box.size = new Vector2(1.5f, 0.62f);
            box.offset = new Vector2(0.72f * m_facingDirection, 0.98f);
        }
        else if (frame == 2)
        {
            //swordHitbox.transform.position = new Vector2(transform.position.x, transform.position.y);
            box.size = new Vector2(1.4f, 0.62f);
            box.offset = new Vector2(-0.2f * m_facingDirection, 0.7f);
        }
    }

    public void AttackHitbox3(int frame)
    {
        swordHitbox.GetComponent<SwordHitbox>().EnableHitbox();
        CapsuleCollider2D box = swordHitbox.GetComponent<CapsuleCollider2D>();
        if (frame == 2)
        {
            //swordHitbox.transform.position = new Vector2(transform.position.x, transform.position.y);
            box.size = new Vector2(1.5f, 1.3f);
            box.offset = new Vector2(0.85f * m_facingDirection, 0.78f);
        }
        else if (frame == 3)
        {
            //swordHitbox.transform.position = new Vector2(transform.position.x, transform.position.y);
            box.size = new Vector2(1.5f, 1.3f);
            box.offset = new Vector2(0.85f * m_facingDirection, 0.7f);
        }
    }

    public void DisableSwordHitbox()
    {
        swordHitbox.GetComponent<SwordHitbox>().DisableHitbox();
    }

    public void castMagic()
    {
        if (m_facingDirection == 1)
            Instantiate(fireballPrefab, fireballStartR.transform.position, Quaternion.identity);
        else
            Instantiate(fireballPrefab, fireballStartL.transform.position, Quaternion.identity);
    }
}
