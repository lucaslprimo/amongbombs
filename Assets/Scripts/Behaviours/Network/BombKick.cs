using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class BombKick : NetworkBehaviour
    {
        [SerializeField] KeyCode key;
        [SerializeField] Transform rotationReference;
        [SerializeField] LayerMask filterRaycastLayer;
        [SerializeField] int kickForce = 10;

        [Header("Events")]
        [SerializeField] UnityEvent onKickBomb;

        private bool shouldKick = false;

        void Update()
        {
            if (isLocalPlayer)
            {
                if (Input.GetKeyDown(key))
                {
                    shouldKick = true;
                }
            }
        }

        private void LateUpdate()
        {
            if (shouldKick)
            {
                shouldKick = false;
                CmdKickBomb();
            }
        }

        [Command]
        private void CmdKickBomb()
        {
            Rigidbody2D rigidbody2D = GetBombAhead();
            if (rigidbody2D)
            {
                RpcOnKickClients();
                rigidbody2D.velocity = new Vector2(rotationReference.right.x, rotationReference.right.y) * kickForce;
            }
        }

        [ClientRpc]
        public void RpcOnKickClients()
        {
            onKickBomb.Invoke();
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
