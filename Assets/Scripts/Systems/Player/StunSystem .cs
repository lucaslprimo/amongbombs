using System;

namespace Primozov.AmongBombs.Systems
{
    public class StunSystem
    {
        private float stunTime = 3;
        private float endStunningTime = 0;
        private bool isStuned = false;

        public StunSystem(float stunTime, float endStunningTime)
        {
            this.stunTime = stunTime;
            this.endStunningTime = endStunningTime;
        }

        public event Action onEnterStunning;
        public event Action onExitStunning;

        public void Update(float time)
        {
            if (isStuned)
            {
                if (time > endStunningTime)
                {
                    onExitStunning.Invoke();
                    isStuned = false;
                }
            }
        }

        public void Stun(float time)
        {
            isStuned = true;
            endStunningTime = time + stunTime;
            onEnterStunning.Invoke();
        }
    }
}
