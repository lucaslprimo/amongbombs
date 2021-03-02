using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Primozov.AmongBombs
{
    public class BombKick : MonoBehaviour
    {
        [SerializeField] KeyCode key;
        [SerializeField] Transform rotationReference;
        [SerializeField] LayerMask filterRaycastLayer;
        [SerializeField] int kickForce = 200;

        public UnityEvent onKickBomb;

        private bool shouldKick = false;

        void Update()
        {
            if (Input.GetKeyDown(key))
            {
               shouldKick = true;   
            }
        }

        private void LateUpdate()
        {
            if (shouldKick)
            {
                shouldKick = false;
                Rigidbody2D rigidbody2D = GetBombAhead();
                if (rigidbody2D)
                {
                    KickBomb(rigidbody2D);
                }
            }
        }

        private void KickBomb(Rigidbody2D rigidbody2D)
        {
            onKickBomb.Invoke();
            rigidbody2D.velocity = new Vector2(rotationReference.right.x, rotationReference.right.y) * kickForce;
        }

        private Rigidbody2D GetBombAhead()
        {
           Rigidbody2D rb = null;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rotationReference.right, 0.6f, filterRaycastLayer);
            if (hit.collider != null)
            {
                rb = hit.collider.attachedRigidbody;
            }

            return rb;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(transform.position, rotationReference.right * 0.6f);
        }
    }
}
