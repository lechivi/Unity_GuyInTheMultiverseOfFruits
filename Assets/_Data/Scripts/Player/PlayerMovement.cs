using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private PlayerCheckWall playerCheckWall;

    [Space(10)]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 12f;

    [Header("DASH")]
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    [Header("EFFECT")]
    [SerializeField] private ParticleSystem moveEffect;
    [SerializeField] private ParticleSystem jumpEffect;
    [SerializeField, Range(0, 10)] private int occurAfterVelocity;
    [SerializeField, Range(0, 0.2f)] private float dustFormationPeriod;

    private enum MovementState { Idle, Running, Jumping, Falling};
    private MovementState movementState;
    private float dirX;
    private float effectTimer;
    private bool isNotDoubleJump;
    private bool canDash = true;
    private bool isDashing;

    public bool IsFacingRight;

    private void Start()
    {
        //this.trailRenderer.emitting = false;
    }

    private void Update()
    {       
        this.Facing();
        if (this.isDashing)
        {
            PlayerDashShadow.instance.ShadowEffect();
            return;
        }

        this.dirX = Input.GetAxisRaw("Horizontal");
      
        this.SpawnMoveEffect();
        this.Jumping();
        this.UpdateAnimations();

        if (Input.GetKeyDown(KeyCode.LeftShift) && this.canDash)
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_DASH);
            }
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (this.isDashing) return;

        this.Moving();
    }

    private void SpawnMoveEffect()
    {
        this.effectTimer += Time.deltaTime;
        if (this.IsGrounded() && Mathf.Abs(this.rb.velocity.x) > occurAfterVelocity && !this.playerCheckWall.IsHitWall)
        {
            if (this.effectTimer > this.dustFormationPeriod)
            {
                this.moveEffect.Play();
                this.effectTimer = 0;
            }
        }
    }

    private void Moving()
    {
        if (this.rb.bodyType == RigidbodyType2D.Static) return;
        this.rb.velocity = new Vector2(this.dirX * this.speed, this.rb.velocity.y);
    }

    private void Jumping()
    {
        if (this.IsGrounded() && !Input.GetButton("Jump"))
        {
            this.isNotDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!this.IsGrounded() && !this.isNotDoubleJump) return;

            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_JUMP);
            }

            this.rb.velocity = new Vector2(this.rb.velocity.x, this.jumpHeight);
            this.isNotDoubleJump = !this.isNotDoubleJump;
            this.animator.SetBool("DoubleJump", !this.isNotDoubleJump);

            if (!this.isNotDoubleJump)
            {
                this.jumpEffect.Play();
            }
        }
    }

    private void UpdateAnimations()
    {
        //if (this.dirX > 0)
        //{
        //    this.spriteRenderer.flipX = false;
        //    this.movementState = MovementState.Running;
        //}
        //else if (this.dirX < 0)
        //{
        //    this.spriteRenderer.flipX = true;
        //    this.movementState = MovementState.Running;
        //}
        if (this.dirX != 0)
        {
            this.movementState = MovementState.Running;
        }
        else
        {
            this.movementState = MovementState.Idle;
        }

        if (this.rb.velocity.y > 0.1)
        {
            this.movementState = MovementState.Jumping;
        }
        else if (this.rb.velocity.y < -0.1)
        {
            this.movementState = MovementState.Falling;
        }

        this.animator.SetInteger("State", (int)this.movementState);

    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(this.col.bounds.center, this.col.bounds.size, 0f, Vector2.down, 0.1f, this.jumpableGround);
    }

    private IEnumerator Dash()
    {
        float originalGravity = this.rb.gravityScale;

        this.canDash = false;
        this.isDashing = true;
        this.rb.gravityScale = 0;
        this.rb.velocity = new Vector2(this.dirX * this.dashingPower, 0f);
        //this.trailRenderer.emitting = true;
        yield return new WaitForSeconds(this.dashingTime);

        //this.trailRenderer.emitting = false;
        this.rb.gravityScale = originalGravity;
        this.isDashing = false;
        yield return new WaitForSeconds(this.dashingCooldown);

        this.canDash = true;
    }

    public void Facing()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < this.transform.position.x)
        {
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.localScale = new Vector3(-1, 1, 1);
            this.IsFacingRight = false;
        }
        else
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            this.IsFacingRight = true;

        }
    }
}
