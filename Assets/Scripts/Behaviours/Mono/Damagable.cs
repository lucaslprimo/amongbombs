using Primozov.AmongBombs.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Primozov.AmongBombs.Behaviours.Mono
{
    public class Damagable : MonoBehaviour
    {
        [SerializeField] bool oneHit;
        [SerializeField] int health = 100;

        [Header("Events")]
        [SerializeField] UnityEvent onDead;
        [SerializeField] UnityEvent<int> onTakeDamage;

        DamageSystem damageSystem;

        private void Awake()
        {
            damageSystem = new DamageSystem(oneHit, health);
            damageSystem.onDead += DamageSystem_onDead;
            damageSystem.onTakeDamage += DamageSystem_onTakeDamage;
        }

        private void DamageSystem_onDead()
        {
            onDead.Invoke();
        }

        private void DamageSystem_onTakeDamage(int damage)
        {
            onTakeDamage.Invoke(damage);
        }

        public void TakeDamage(int damage)
        {
            damageSystem.TakeDamage(damage);
        }
    }
}
