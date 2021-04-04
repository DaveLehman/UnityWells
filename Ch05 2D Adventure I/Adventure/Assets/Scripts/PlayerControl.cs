using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour
{
    public enum FACEDIRECTION { FACELEFT = -1, FACERIGHT = 1 };
    public FACEDIRECTION Facing = FACEDIRECTION.FACERIGHT;
    public LayerMask GroundLayer;
    public CircleCollider2D FeetCollider = null;
    public bool isGrounded = false;
    public string HorzAxis = "Horizontal";
    public string JumpButton = "Jump";
    public float MaxSpeed = 50f;
    public float JumpPower = 600;
    public float JumpTimeOut = 1f;
    public bool CanControl = true;
    public static PlayerControl PlayerInstance = null;
    public static float Health
    {
        get
        {
            return _Health;
        }

        set
        {
            _Health = value;

            if (_Health <= 0)
            {
                Die();
            }
        }
    }

    [SerializeField] private static float _Health = 100f;
    private Rigidbody2D ThisBody = null;
    private bool CanJump = true;

    void Awake()
    {
        ThisBody = GetComponent<Rigidbody2D>();

        PlayerInstance = this;
    }

    private bool GetGrounded()
    {
        // detects where any CircleCollider intersects and overlaps with any other collider in the scene on a specific layer.
        // This function indicates whether the player is touching the ground, and if they are, the player can jump.
        // Otherwise, the player cannot jump aas tehy are already airborne. Double-jumping is not allowed in this game.
        Vector2 CircleCenter = new Vector2(transform.position.x, transform.position.y) + FeetCollider.offset;
        Collider2D[] HitColliders = Physics2D.OverlapCircleAll(CircleCenter, FeetCollider.radius, GroundLayer);
        return HitColliders.Length > 0;
    }

    private void Jump()
    {
        if (!isGrounded || !CanJump) return;
        ThisBody.AddForce(Vector2.up * JumpPower);
        CanJump = false;
        Invoke("ActivateJump", JumpTimeOut);
    }
    private void ActivateJump()
    {
        CanJump = true;
    }

    void FixedUpdate()
    {
        /* FixedUpdate instead of Update because we're working with RigidBody2D - a physics-based component. All physics functionality
         * should be updated in FixedUpdate. The movement and motion of a player is set using RigidBody2D.
         * */
        if (!CanControl || Health <= 0f)
        {
            return;
        }

        isGrounded = GetGrounded();
        float Horz = CrossPlatformInputManager.GetAxis(HorzAxis);
        ThisBody.AddForce(Vector2.right * Horz * MaxSpeed);

        if (CrossPlatformInputManager.GetButton(JumpButton))
        {
            Jump();
        }

        ThisBody.velocity = new Vector2(Mathf.Clamp(ThisBody.velocity.x, -MaxSpeed, MaxSpeed),
            Mathf.Clamp(ThisBody.velocity.y, -Mathf.Infinity, JumpPower));

        if ((Horz < 0f && Facing != FACEDIRECTION.FACELEFT) || (Horz > 0f && Facing != FACEDIRECTION.FACERIGHT))
        {
            FlipDirection();
        }
    }
    private void FlipDirection()
    {
        Facing = (FACEDIRECTION)((int)Facing * -1f);
        Vector3 LocalScale = transform.localScale;
        LocalScale.x *= -1f;
        transform.localScale = LocalScale;
    }
    void OnDestroy()
    {
        PlayerInstance = null;
    }

    static void Die()
    {
        Destroy(PlayerControl.PlayerInstance.gameObject);
    }

    public static void Reset()
    {
        Health = 100f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
