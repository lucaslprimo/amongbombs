using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Primozov.AmongBombs.Behaviours.Mono
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] UnityEvent<GameObject> onCollectedBy;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                onCollectedBy.Invoke(collision.gameObject);
                Destroy(gameObject, 1f);
            }
        }
    }
}
