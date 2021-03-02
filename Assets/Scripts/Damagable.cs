using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Primozov.AmongBombs
{
    public class Damagable : MonoBehaviour
    {
        [SerializeField] bool oneHit;
        [SerializeField] int health = 100;

        [SerializeField] UnityEvent onDead;
        [SerializeField] UnityEvent<int> onTakeDamage;

        public void TakeDamage(int damage)
        {
            if (oneHit)
            {
                onDead.Invoke();
            }
            else
            {
                health -= damage;
                if (health <= 0) {
                    onDead.Invoke();
                }
                else
                {
                    onTakeDamage.Invoke(damage);
                }
            }
        }
    }
}
