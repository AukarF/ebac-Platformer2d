using DG.Tweening;
using UnityEngine;

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

    [Header("Jump Colission Check")]
    public Collider2D collider2D;
    public float disToGround;
    public float spaceToGround = .1f;
    public ParticleSystem jumpVFX;


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

    private bool IsGrounded()
    {
        //Debug.DrawRay(transform.position, -Vector2.up, Color.magenta, disToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, -Vector2.up, disToGround + spaceToGround);

    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;

        animator.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void Update()
    {
        IsGrounded();
        HandleJump();
        HandleMovement();
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
   
            private void HandleJump()
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
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
    Tweener tween;
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
}


