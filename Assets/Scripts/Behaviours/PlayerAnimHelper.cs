using Unity.Netcode;
using UnityEngine;

namespace Primozov.AmongBombs.Behaviours.Mono
{
    public class PlayerAnimHelper : NetworkBehaviour
    {
        [SerializeField] Animator animator;

        private const string PARAM_X_MOVE = "xMove";
        private const string PARAM_Y_MOVE = "yMove";
        private const string PARAM_IS_WALKING = "isWalking";

        private const string PARAM_SPEED = "speed";


        public void UpdateMove(Vector2 movementVector)
        {
            animator.SetInteger(PARAM_X_MOVE, (int)movementVector.x);
            animator.SetInteger(PARAM_Y_MOVE, (int)movementVector.y);

            animator.SetBool(PARAM_IS_WALKING, movementVector != Vector2.zero);
        }


        public void UpdateSpeed()
        {
            animator.SetFloat(PARAM_SPEED, animator.GetFloat(PARAM_SPEED) + 0.2f);
        }
    }
}
