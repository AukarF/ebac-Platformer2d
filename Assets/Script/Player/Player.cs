using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myrigidbody;
    public HealthBase healthBase;

    [Header("Setup")]
    public SOPlayer soPlayerSetup;

    public Animator animator;



    private float _currentSpeed;
    //private bool _isRunning = false;

   

    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }       
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;

        animator.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void Update()
    {
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
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myrigidbody.MovePosition(myrigidbody.position + velocity * Time.deltaTime);
            myrigidbody.linearVelocity = new Vector2(_currentSpeed, myrigidbody.linearVelocity.y);
            if (myrigidbody.transform.localScale.x != -1)
            {
                myrigidbody.transform.DOScaleX(1, soPlayerSetup.animationSwipeDuration);
            }
            animator.SetBool(soPlayerSetup.boolRun, true);
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myrigidbody.linearVelocity = Vector2.up * soPlayerSetup.forceJump;
            myrigidbody.transform.localScale = Vector2.one;

            DOTween.Kill(myrigidbody.transform);

            HandleScaleJump();
        }
            
    }

    private void HandleScaleJump()
    {
        myrigidbody.transform.DOScaleY(soPlayerSetup.jumpScaley, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myrigidbody.transform.DOScaleX(soPlayerSetup.jumpScalex, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}


