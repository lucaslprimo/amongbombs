using System;
using UnityEngine;

namespace Primozov.AmongBombs.Systems
{
    public class BombSystem
    {
        private float explosionTime = 0;
        private int bombRange;
      
        private bool exploded = false;

        public event Action onExplode;

        public BombSystem(int bombRange, float secondsToExplode)
        {
            explosionTime = Time.time + secondsToExplode;
            this.bombRange = bombRange;
        }
       
        public void Update(float time)
        {
            if (time > explosionTime)
            {
                Explode();
            }
        }

        public int GetBombRange()
        {
            return bombRange;
        }

        public void Explode()
        {
            if (!exploded)
            {
                exploded = true;
                onExplode.Invoke();
            }
        }
    }
}
