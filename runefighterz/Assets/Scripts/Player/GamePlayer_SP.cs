using UnityEngine;

public class GamePlayer_SP : MonoBehaviour
{
    public bool m_FacingRight = true;

    [SerializeField] private float m_MaxSpeed = 10f;
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private LayerMask m_WhatIsPlatform;

    [HideInInspector] public Transform m_GroundCheck;
    const float k_GroundedRadius = 0.2f;
    [SerializeField] public bool m_Grounded;
    [SerializeField] private bool m_OnPlatform;
    [HideInInspector] public Animator m_Anim;
    [HideInInspector] public Rigidbody2D m_Rigidbody2D;
    private bool m_Jump;

    void Awake()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        //gameObject.GetComponent<BoxCollider2D>().size = S;
        //gameObject.GetComponent<BoxCollider2D>().offset = new Vector2((S.x / 2), S.y / 2);

        if (!m_Jump)
        {
            m_Jump = Input.GetButtonDown("Jump");
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        Move(h, m_Jump);
        m_Jump = false;

        m_Grounded = false;
        m_OnPlatform = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }

        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsPlatform);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_OnPlatform = true;
        }
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
                        LayerMask.NameToLayer("OneWayPlatform"),
                        m_Grounded || m_Rigidbody2D.velocity.y > 0f || !m_OnPlatform && !m_Grounded
                       );
        if (m_Grounded && m_OnPlatform)
        {
            m_Anim.SetBool("Ground", true);
        }
        if (m_Grounded && !m_OnPlatform)
        {
            m_Anim.SetBool("Ground", true);
        }
        if (!m_Grounded && m_OnPlatform)
        {
            m_Anim.SetBool("Ground", true);
        }
        if (!m_Grounded && !m_OnPlatform)
        {
            m_Anim.SetBool("Ground", false);
        }
    }

    public void Move(float move, bool jump)
    {
        if (m_Grounded || m_AirControl)
        {
            m_Anim.SetFloat("Speed", Mathf.Abs(move));

            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

            if (move > 0 && !m_FacingRight)
            {
                m_FacingRight = !m_FacingRight;
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                m_FacingRight = !m_FacingRight;
                Flip();
            }
        }
        if (m_Grounded && jump && m_Anim.GetBool("Ground"))
        {
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            JumpSound();
        }
    }
    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void JumpSound()
    {
        GetComponentInParent<lucy_spells_sp>().PlayJumpSound();
    }
}