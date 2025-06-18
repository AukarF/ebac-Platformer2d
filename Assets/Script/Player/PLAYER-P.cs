//using UnityEngine;
//using DG.Tweening;
//using System.Collections;

//public class Player : MonoBehaviour
//{
//    public Rigidbody2D myrigidbody;
//    public HealthBase healthBase;
//    public SOPlayer soPlayerSetup;
//    public Animator animator;

//    [Header("Jump Check")]
//    public Collider2D collider2D;
//    public float spaceToGround = 0.1f;
//    public ParticleSystem jumpVFX;

//    [Header("Slide Settings")]
//    public float slideDuration = 0.5f;
//    public float slideSpeedMultiplier = 1.5f;

//    private float disToGround;
//    private Vector3 initialPosition;
//    private int _playerDirection = 1;
//    private float _currentSpeed;
//    private Tweener tween;
//    private bool isSliding = false;

//    [Header("Jump Physics")]
//    public float normalGravity = 1f;
//    public float fallGravity = 3f;
//    public float jumpForce = 12f;

//    private void Awake()
//    {
//        if (collider2D != null)
//            disToGround = collider2D.bounds.extents.y;

//        initialPosition = transform.position;

//        if (healthBase != null)
//        {
//            healthBase.OnKill += OnPlayerKill;

//            if (healthBase.CurrentHealth <= 0)
//                healthBase.ResetHealth();
//        }
//    }

//    private void Update()
//    {
//        //LockXPosition();    //manter o personagem fixo na camera
//        HandleMovement();   //animacao de correr
//        HandleJump();       // input de pulo
//        HandleSlide();      //input de slide
//    }

//    //private void LockXPosition()
//   // {
//        //transform.position = new Vector3(initialPosition.x, transform.position.y, transform.position.z);
//        //myrigidbody.linearVelocity = new Vector2(0f, myrigidbody.linearVelocity.y);
//    //}

//    private void HandleMovement()
//    {
//        animator.speed = 1f;

//        if (myrigidbody.transform.localScale.x != 1)
//            myrigidbody.transform.DOScaleX(1, soPlayerSetup.animationSwipeDuration);

//        animator.SetBool(soPlayerSetup.boolRun, true);
//        _playerDirection = 1;

//         if (Input.GetKeyDown(KeyCode.RightArrow))
//         {
//            //myrigidbody.MovePosition(myrigidbody.position + velocity * Time.deltaTime);
//            myrigidbody.linearVelocity = new Vector2(_currentSpeed, myrigidbody.linearVelocity.y);
//            if (myrigidbody.transform.localScale.x != 1)
//            {
//                myrigidbody.transform.DOScaleX(1, soPlayerSetup.animationSwipeDuration);
//            }
//            animator.SetBool(soPlayerSetup.boolRun, true);
//            _playerDirection = 1;
//         }
//    }

//    private void HandleJump()
//    {
//        if (Input.GetKeyDown(KeyCode.UpArrow))
//        {
//            //reativar isso quando o terreno estiver com Layer "Ground"
//             if (!IsGrounded()) return;

//            Debug.Log("Pulando (sem checar chão)");
//            myrigidbody.linearVelocity = new Vector2(myrigidbody.linearVelocity.x, 0); //zera o Y pra nao somar força com o pulo anterior
//            myrigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //deixa o pulo baseado em impulso, mais realista.
//            DOTween.Kill(myrigidbody.transform);
//            if (tween != null) tween.Kill();

//            HandleScaleJump();
//            PlayerJumpVFX();

//            animator.SetTrigger("Jump");
//        }
//    }

//    private void HandleSlide()
//    {
//        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
//        {
//            Debug.Log("Iniciando slide");
//            StartCoroutine(Slide());
//        }
//    }

//    private IEnumerator Slide()
//    {
//        isSliding = true;

//        if (animator != null)
//            animator.SetTrigger("Slide");

//        // Acelera objetos com AutoMove
//        AutoMove[] movingObjects = FindObjectsOfType<AutoMove>();
//        foreach (var obj in movingObjects)
//            obj.speed *= slideSpeedMultiplier;

//        yield return new WaitForSeconds(slideDuration);

//        foreach (var obj in movingObjects)
//            obj.speed /= slideSpeedMultiplier;

//        isSliding = false;
//    }

//    private void HandleScaleJump()
//    {
//        myrigidbody.transform.DOScaleY(soPlayerSetup.jumpScaley, soPlayerSetup.animationDuration)
//            .SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);

//        tween = DOTween.To(() => myrigidbody.transform.localScale.x,
//                           value => myrigidbody.transform.localScale = new Vector3(value * _playerDirection,
//                            myrigidbody.transform.localScale.y,
//                            myrigidbody.transform.localScale.z),
//                            soPlayerSetup.jumpScalex,
//                            soPlayerSetup.animationDuration)
//                        .SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
//    }

//    private void PlayerJumpVFX()
//    {
//        if (jumpVFX != null)
//            jumpVFX.Play();
//    }

//    private void OnPlayerKill()
//    {
//        if (animator != null)
//            animator.SetTrigger(soPlayerSetup.triggerDeath);

//        if (healthBase != null)
//            healthBase.OnKill -= OnPlayerKill;
//    }

//    public void DestroyMe()
//    {
//        Destroy(gameObject);
//    }

//      //Desativado temporariamente, só usar quando tiver Ground Layer configurada
    
//    private bool IsGrounded()
//    {
//        Vector2 origin = collider2D.bounds.center;
//        origin.y = collider2D.bounds.min.y;
//        Debug.DrawRay(origin, Vector2.down * spaceToGround, Color.magenta);
//        return Physics2D.Raycast(origin, Vector2.down, spaceToGround, LayerMask.GetMask("Ground"));
//    }
    
//    private void FixedUpdate()
//    {
//        if (myrigidbody.linearVelocity.y < 0)
//        {
//            myrigidbody.gravityScale = fallGravity; //mais gravidade ao cair (cai mais rapido)
//        }
//        else
//        {
//            myrigidbody.gravityScale = normalGravity; //gravidade normal quando nao tem trigger
//        }
//    }
//} 
  