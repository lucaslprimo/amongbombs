using System;

namespace Primozov.AmongBombs.Systems
{
    public class DamageSystem
    {
        bool oneHit;
        int health = 100;

        public event Action onDead;
        public event Action<int> onTakeDamage;

        public DamageSystem(bool oneHit, int health)
        {
            this.oneHit = oneHit;
            this.health = health;
        }

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
