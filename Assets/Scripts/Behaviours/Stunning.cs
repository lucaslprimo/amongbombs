using Primozov.AmongBombs.Systems;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace Primozov.AmongBombs.Behaviours.Mono
{
    public class Stunning : NetworkBehaviour
    {
        [SerializeField] float stunTime = 3;

        private float endStunningTime = 0;

        [Header("Events")]
        [SerializeField] UnityEvent onEnterStunning;
        [SerializeField] UnityEvent onExitStunning;

        StunSystem stunSystem;

        private void Awake()
        {
            stunSystem = new StunSystem(stunTime, endStunningTime);
            stunSystem.onEnterStunning += HandleEnterStunning;
            stunSystem.onExitStunning += HandleExitStunning;
        }

        private void HandleEnterStunning()
        {
            onEnterStunning.Invoke();
        }

        private void HandleExitStunning()
        {
            onExitStunning.Invoke();
        }

        private void Update()
        {
            stunSystem.Update(Time.time);
        }

        public void Stun()
        {
            stunSystem.Stun(Time.time);
        }
    }
}
