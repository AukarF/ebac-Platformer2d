using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayer : ScriptableObject
{
    public Animator animator;
    public SOString soStringName;
    
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
    public string triggerDeath = "Death";
    public float animationSwipeDuration = .1f;

    [Header("Jump Colission Check")]
    public Collider2D collider2D;
    public float disToGround;
    public float spaceToGround = .1f;
    public ParticleSystem jumpVFX;

}
