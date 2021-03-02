using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Primozov.AmongBombs
{
    public class Stunning : MonoBehaviour
    {
        [SerializeField] float stunTime = 3;

        private float endStunningTime = 0;
        private bool isStuned = false;

        [SerializeField] UnityEvent onEnterStunning;
        [SerializeField] UnityEvent onExitStunning;

        private void Update()
        {
            if (isStuned)
            {
                if (Time.time > endStunningTime)
                {
                    onExitStunning.Invoke();
                    isStuned = false;
                }
            }

        }

        public void Stun()
        {
            isStuned = true;
            endStunningTime = Time.time + stunTime;
            onEnterStunning.Invoke();
        }
    }
}
