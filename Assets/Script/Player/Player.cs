using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myrigidbody;
    
    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2f;

    [Header("Animation Setup")]
    public float jumpScaley = 1.5f;
    public float jumpScalex = 0.7f;
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;

    [Header("Animation.Player")]
    public string boolRun = "Run";
    public Animator animator;
    public float animationSwipeDuration = .1f;

    private float _currentSpeed;
    private bool _isRunning = false;

    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = speedRun;
            animator.speed = 2f;
        }
        else
        {
            _currentSpeed = speed;
            animator.speed = 1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myrigidbody.MovePosition(myrigidbody.position - velocity * Time.deltaTime);
            myrigidbody.linearVelocity = new Vector2(-_currentSpeed, myrigidbody.linearVelocity.y);
            if (myrigidbody.transform.localScale.x != -1)
            {
                myrigidbody.transform.DOScaleX(-1, animationSwipeDuration);
            }
            animator.SetBool(boolRun, true);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myrigidbody.MovePosition(myrigidbody.position + velocity * Time.deltaTime);
            myrigidbody.linearVelocity = new Vector2(_currentSpeed, myrigidbody.linearVelocity.y);
            if (myrigidbody.transform.localScale.x != -1)
            {
                myrigidbody.transform.DOScaleX(1, animationSwipeDuration);
            }
            animator.SetBool(boolRun, true);
        }
        else
        {
            animator.SetBool(boolRun, false);
        }

        Debug.Log(myrigidbody.linearVelocity);

        if (myrigidbody.linearVelocity.x > 0)
        {
            myrigidbody.linearVelocity -= friction;
        }
        else if (myrigidbody.linearVelocity.x < 0)
        {
            myrigidbody.linearVelocity += friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myrigidbody.linearVelocity = Vector2.up * forceJump;
            myrigidbody.transform.localScale = Vector2.one;

            DOTween.Kill(myrigidbody.transform);

            HandleScaleJump();
        }
            
    }

    private void HandleScaleJump()
    {
        myrigidbody.transform.DOScaleY(jumpScaley, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myrigidbody.transform.DOScaleX(jumpScalex, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}


