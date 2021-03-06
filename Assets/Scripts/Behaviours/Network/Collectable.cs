using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class Collectable : NetworkBehaviour
    {
        [SerializeField] UnityEvent<GameObject> onCollectedBy;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (PlayerNetwork.Instance.isLocalPlayer)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    onCollectedBy.Invoke(collision.gameObject);
                }
            }
        }
    }
}
