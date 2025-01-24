using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimatorTest : MonoBehaviour
{
    public Animator animator;

    public KeyCode keyToTrigger = KeyCode.A;
    public KeyCode keyToExit = KeyCode.S;
    public string triggerToPlay = "Fly";



    private void OnValidate()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKeyDown(keyToTrigger))
        {
            animator.SetBool(triggerToPlay, true);   
        }
        else if (Input.GetKeyDown(keyToExit))
        {
            animator.SetBool(triggerToPlay, false);
        }
    }
}
