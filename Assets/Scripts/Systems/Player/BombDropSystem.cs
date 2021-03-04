using System.Collections.Generic;

namespace Primozov.AmongBombs.Systems
{
    public class BombDropSystem
    {
        float secondsToExplode = 3;
        int bombLimit = 10;
        int rangeLimit = 10;

        private List<float> bombDropCooldownTimes = new List<float>();
        private int bombRange = 0;
        private int bombIndex = 0;

        public BombDropSystem(float secondsToExplode, int bombLimit, int rangeLimit)
        {
            this.secondsToExplode = secondsToExplode;
            this.bombLimit = bombLimit;
            this.rangeLimit = rangeLimit;
            IncreaseBombs();
            IncreaseRange();
        }

        public void IncreaseRange()
        {
            bombRange++;
        }

        public void IncreaseBombs()
        {
            if(bombDropCooldownTimes.Count < bombLimit)
                bombDropCooldownTimes.Add(0);
        }

        public void IncreaseToMaxRange()
        {
            if (bombRange < rangeLimit)
            {
                bombRange = rangeLimit - bombRange;
            }
        }

        public void IncreaseToMaxBombs()
        {
            if (bombDropCooldownTimes.Count < bombLimit)
            {
                for (int i = 0; i < bombLimit - bombDropCooldownTimes.Count; i++)
                {
                    bombDropCooldownTimes.Add(0);
                }
            }
        }

        public bool CanDropBomb(float time)
        {
            return time > bombDropCooldownTimes[bombIndex];
        }

        public void BombDroped(float time)
        {
            bombDropCooldownTimes[bombIndex] = time + secondsToExplode;
            bombIndex++;

            if (bombIndex == bombDropCooldownTimes.Count)
            {
                bombIndex = 0;
            }
        }

        public int GetBombRange()
        {
            return bombRange;
        }
    }
}
