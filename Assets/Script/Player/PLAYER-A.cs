using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    public Rigidbody2D myrigidbody;
    public HealthBase healthBase;

    [Header("Setup")]
    public SOPlayer soPlayerSetup;

    public Animator animator;


    private int _playerDirection = 1;
    private float _currentSpeed;
    //private bool _isRunning = false;

    [Header("Slide Settings")]
    public float slideDuration = 0.5f;
    public float slideSpeedMultiplier = 1.5f;

    private float disToGround;
    private Vector3 initialPosition;
    //private int _playerDirection = 1;
    //private float _currentSpeed;
    private Tweener tween;
    private bool isSliding = false;

    [Header("Jump Colission Check")]
    public Collider2D collider2D;
    //public float disToGround;
    public float spaceToGround = .1f;
    public ParticleSystem jumpVFX;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    [Header("Jump Physics")]
    public float normalGravity = 1f;
    public float fallGravity = 3f;
    public float jumpForce = 12f;

    bool grounded;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        if (collider2D != null)
        {
            disToGround = collider2D.bounds.extents.y;
        }
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;

        animator.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void Update()
    {
        //IsGrounded();
        HandleMovement();   //animacao de correr
        HandleJump();       // input de pulo
        HandleSlide();      //input de slide
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = soPlayerSetup.speedRun;
            animator.speed = 2f;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            animator.speed = 1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myrigidbody.MovePosition(myrigidbody.position - velocity * Time.deltaTime);
            myrigidbody.linearVelocity = new Vector2(-_currentSpeed, myrigidbody.linearVelocity.y);
            if (myrigidbody.transform.localScale.x != -1)
            {
                myrigidbody.transform.DOScaleX(-1, soPlayerSetup.animationSwipeDuration);
            }
            animator.SetBool(soPlayerSetup.boolRun, true);
            _playerDirection = -1;
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myrigidbody.MovePosition(myrigidbody.position + velocity * Time.deltaTime);
            myrigidbody.linearVelocity = new Vector2(_currentSpeed, myrigidbody.linearVelocity.y);
            if (myrigidbody.transform.localScale.x != 1)
            {
                myrigidbody.transform.DOScaleX(1, soPlayerSetup.animationSwipeDuration);
            }
            animator.SetBool(soPlayerSetup.boolRun, true);
            _playerDirection = 1;
        }
        else
        {
            animator.SetBool(soPlayerSetup.boolRun, false);
        }



        if (myrigidbody.linearVelocity.x > 0)
        {
            myrigidbody.linearVelocity -= soPlayerSetup.friction;
        }
        else if (myrigidbody.linearVelocity.x < 0)
        {
            myrigidbody.linearVelocity += soPlayerSetup.friction;
        }
    }

    private void HandleSlide()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
        {
            Debug.Log("Iniciando slide");
            StartCoroutine(Slide());
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;

            if (animator != null)
                animator.SetTrigger("Slide");

            // Acelera objetos com AutoMove
            AutoMove[] movingObjects = FindObjectsOfType<AutoMove>();
            foreach (var obj in movingObjects)
                obj.speed *= slideSpeedMultiplier;

            yield return new WaitForSeconds(slideDuration);

            foreach (var obj in movingObjects)
                obj.speed /= slideSpeedMultiplier;

            isSliding = false;
    }

        //LockXPosition();    //manter o personagem fixo na camera


    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) //&& //IsGrounded())
        {
            myrigidbody.linearVelocity = Vector2.up * soPlayerSetup.forceJump;
            myrigidbody.transform.localScale = Vector2.one;

            DOTween.Kill(myrigidbody.transform);
            if (tween != null) tween.Kill();

            HandleScaleJump();
            PlayerJumpVFX();
        }
    }

    private void PlayerJumpVFX()
    {
        if (jumpVFX != null) jumpVFX.Play();



    }
    //Tweener tween;
    private void HandleScaleJump()
    {
        myrigidbody.transform.DOScaleY(soPlayerSetup.jumpScaley, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        tween = DOTween.To(ScaleXGetter, ScaleXSetter, soPlayerSetup.jumpScalex, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    private float ScaleXGetter()
    {
        return myrigidbody.transform.localScale.x;
    }

    private void ScaleXSetter(float value)
    {
        var s = myrigidbody.transform.localScale;
        s.x = value * _playerDirection;
        myrigidbody.transform.localScale = s;
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    //public bool IsGrounded()
    //{
    //    if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GROUND"))
        {
            Vector3 normal = other.GetContact(0).normal;
            if (normal == Vector3.up)
            {
                grounded = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GROUND"))
        {
            grounded = false;
        }

    }
    private void FixedUpdate()
    {
        if (myrigidbody.linearVelocity.y < 0)
        {
            myrigidbody.gravityScale = fallGravity; //mais gravidade ao cair (cai mais rapido)
        }
        else
        {
            myrigidbody.gravityScale = normalGravity; //gravidade normal quando nao tem trigger
        }
    }
}


