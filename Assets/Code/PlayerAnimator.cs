using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Player_Properties playerProperties;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerProperties.IsGrounded())
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);

            if (playerProperties.GetCurrentVelocity() != Vector3.zero)
            {
                if (playerProperties.speed == playerProperties.walkSpeed)
                {
                    animator.SetBool("Walking", true);
                }
                else
                {
                    animator.SetBool("Running", true);

                }
            }
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);

        }
    }
}
