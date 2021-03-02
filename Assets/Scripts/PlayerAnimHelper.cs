using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimHelper : MonoBehaviour
{
    [SerializeField] Animator animator;

    private const string PARAM_X_MOVE = "xMove";
    private const string PARAM_Y_MOVE = "yMove";
    private const string PARAM_IS_WALKING = "isWalking";

    public void UpdateMove(Vector2 movementVector)
    {
        animator.SetInteger(PARAM_X_MOVE, (int)movementVector.x);
        animator.SetInteger(PARAM_Y_MOVE, (int)movementVector.y);

        animator.SetBool(PARAM_IS_WALKING, movementVector != Vector2.zero);
    }
}
