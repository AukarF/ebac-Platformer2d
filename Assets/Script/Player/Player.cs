using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    public Rigidbody2D myrigidbody;

    public Vector2 friction = new Vector2(.1f, 0);

    public float speed;
    public float speedRun;

    public float forceJump = 2f;

    private float _currentSpeed;

    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            _currentSpeed = speedRun;
        else
            _currentSpeed = speed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myrigidbody.MovePosition(myrigidbody.position - velocity * Time.deltaTime);
            myrigidbody.linearVelocity = new Vector2(-_currentSpeed, myrigidbody.linearVelocity.y);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myrigidbody.MovePosition(myrigidbody.position + velocity * Time.deltaTime);
            myrigidbody.linearVelocity = new Vector2(_currentSpeed, myrigidbody.linearVelocity.y);
        }


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
            myrigidbody.linearVelocity = Vector2.up * forceJump;
    }
}


