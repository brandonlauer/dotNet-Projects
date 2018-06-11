using System;
using UnityEngine;
using UnityEngine.Networking;

public class Character2D : NetworkBehaviour {

    [Header("Player stats")]
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsSolidGround;

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private NetworkAnimator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;

    [HideInInspector] public bool Stunned = false;
    private Transform playerGraphics; // Reference to the player graphics to change direction.
    private Transform statusBar;
    private Vector3 statusBarScale;
    private float statusX;
    private float x;
    [SyncVar(hook ="OnSetScale")]private Vector3 ls;

    public bool onSolidGround = false;

    [Header("Weapon stats")]
    public float FireRate = 0;
    public GameObject LaserPrefab;
    public float ProjectileSpeed = 10f;
    public int Damage = 10;
    public Transform FirePoint;
    public Transform StartPoint;

    private float timeToFire = 0;

    [Header("Player arm")]
    public int rotationOffset = 90;
    public Transform forearm;
    public Transform backarm;

    [Header ("Shield Drawing")]
    public Transform DrawPoint;
    public GameObject ShieldBlock;
    public float DrawRate, RefillRate, DrainRate;
    public float BlockLifeTime = 3;
    public Transform SpawnParticles;

    private float maxShieldAmount = 100f;
    private float currentShieldAmount;
    private float timeToDraw = 0;
    [HideInInspector]
    public bool shieldObstructed = false;
    private Transform shieldFuelBar;

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<CameraFollow>().SetTarget(this.gameObject.transform);
        shieldFuelBar = Camera.main.transform.Find ("GUI").Find("ShieldBar");
        gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
        gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(1, true);
        gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(2, true);
        gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(3, true);
        gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(4, true);
        gameObject.GetComponent<NetworkAnimator>().SetParameterAutoSend(5, true);
    }

    private void Start()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Anim = GetComponent<NetworkAnimator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerGraphics = transform.Find("Graphics");
        statusBar = transform.Find("StatusIndicator");
        statusBarScale = statusBar.GetComponent<RectTransform>().localScale;
        statusX = statusBarScale.x;

        currentShieldAmount = maxShieldAmount;

        if (playerGraphics == null)
        {
            Debug.LogError("There is no graphics object as child object of player.");
        }

        if (FirePoint == null)
        {
            Debug.LogError ("There is no fire point!");
        }
        if (StartPoint == null)
        {
            Debug.LogError ("There is no start point!");
        }

        x = transform.localScale.x;
        CmdSetScale(transform.localScale);
        ls = transform.localScale;
    }


    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if(Stunned)
        {
            return;
        }
        m_Grounded = false;
        onSolidGround = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsSolidGround);
        for (int i = 0; i < colliders2.Length; i++)
        {
            if (colliders2[i].gameObject != gameObject)
            {
                onSolidGround = true;
            }
        }
        m_Anim.animator.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        m_Anim.animator.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        if (dir.x >= 0)
        {
            CmdSetScale(new Vector3(x, ls.y, ls.z));
            //ls.x = x;
            statusBarScale.x = statusX;
            statusBar.GetComponent<RectTransform>().localScale = statusBarScale;
            //transform.localScale = ls;
            if (m_Rigidbody2D.velocity.x < 0)
            {
                m_Anim.animator.SetBool("Backwards", true);
            }
            else
            {
                m_Anim.animator.SetBool("Backwards", false);
            }
        }
        else
        {
            CmdSetScale(new Vector3(-x, ls.y, ls.z));
            //ls.x = -x;
            //transform.localScale = ls;
            statusBarScale.x = -statusX;
            statusBar.GetComponent<RectTransform>().localScale = statusBarScale;
            if (m_Rigidbody2D.velocity.x > 0)
            {
                m_Anim.animator.SetBool("Backwards", true);
            }
            else
            {
                m_Anim.animator.SetBool("Backwards", false);
            }
        }

        // Arm rotation
        Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position; // Subtracting the player position from the mouse position.
        difference.Normalize (); // Normalizing the vector so that the sum of the vector will be equal to 1.

        float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg; // Find the angle in degrees
        if (transform.localScale.x > 0)
        {
            forearm.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
            backarm.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        else
        {
            forearm.rotation = Quaternion.Euler (0f, 0f, rotZ - rotationOffset + 180);
            backarm.rotation = Quaternion.Euler(0f, 0f, rotZ + 180);
        }

        // Blaster code
        if (FireRate == 0)
        {
            if (Input.GetButtonDown ("Fire1"))
            {
                CmdFire ();
            }
        }
        else
        {
            if (Input.GetButton ("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / FireRate;
                CmdFire ();
            }
        }

        // Shield drawing code
        if (onSolidGround)
        {
            if (currentShieldAmount < maxShieldAmount)
            {
                currentShieldAmount += RefillRate;
            }
        }
        shieldFuelBar.transform.localScale = new Vector3 (currentShieldAmount / maxShieldAmount, shieldFuelBar.transform.localScale.y);

        if (shieldObstructed)
        {
            return;
        }

        if (Input.GetButton ("Fire2") && Time.time > timeToDraw && currentShieldAmount > 1)
        {
            currentShieldAmount -= DrainRate;
            timeToDraw = Time.time + 1 / DrawRate;
            CmdDrawShield ();
        }
    }

    [Command]
    void CmdDrawShield()
    {
        GameObject block = Instantiate (ShieldBlock, DrawPoint.position, transform.rotation);
        Transform particles = Instantiate (SpawnParticles, DrawPoint.position, transform.rotation);
        Destroy (particles.gameObject, 0.5f);
        Destroy (block, BlockLifeTime);
        NetworkServer.Spawn (block);
    }

    [Command]
    void CmdFire()
    {
        Vector2 direction = FirePoint.position - StartPoint.position;
        direction.Normalize ();
        Quaternion rotation = Quaternion.Euler (0, 0, Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg);

        GameObject laser = (GameObject)Instantiate (LaserPrefab, FirePoint.position, rotation);
        laser.GetComponent<Projectile> ().parent = this.transform;
        laser.GetComponent<Projectile> ().Damage = Damage;
        laser.GetComponent<Rigidbody2D> ().velocity = direction * ProjectileSpeed;
        NetworkServer.Spawn (laser);
    }

    [Command]
    void CmdSetScale(Vector3 scale)
    {
        ls = scale;
    }

    private void OnSetScale(Vector3 scale)
    {
        ls = scale;
        transform.localScale = scale;
    }

    public void Move(float move, bool crouch, bool jump)
    {
        if (Stunned)
        {
            return;
        }
        // If crouching, check to see if the character can stand up
        if (!crouch && m_Anim.animator.GetBool("Crouch"))
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        // Set whether or not the character is crouching in the animator
        m_Anim.animator.SetBool("Crouch", crouch);

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Reduce the speed if crouching by the crouchSpeed multiplier
            move = (crouch ? move * m_CrouchSpeed : move);

            // The Speed animator parameter is set to the absolute value of the horizontal input.
            m_Anim.animator.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
        }
        // If the player should jump...
        if (m_Grounded && jump && m_Anim.animator.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Anim.animator.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }
}
