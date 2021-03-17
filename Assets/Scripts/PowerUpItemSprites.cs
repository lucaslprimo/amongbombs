using UnityEditor;
using UnityEngine;

namespace Primozov.AmongBombs
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PowerUps", order = 1)]
    public class PowerUpItemSprites: ScriptableObject
    {
       [SerializeField] Sprite addBombSprite; 
       [SerializeField] Sprite addRangeSprite; 
       [SerializeField] Sprite addSpeedSprite; 
       [SerializeField] Sprite addInvertAxisSprite; 
       [SerializeField] Sprite bombKickSprite;

        public Sprite GetSpriteByType(PowerUpType powerUpType)
       {
            switch (powerUpType)
            {
                case PowerUpType.AddBomb:
                    return addBombSprite;
                case PowerUpType.AddRange:
                    return addRangeSprite;
                case PowerUpType.AddSpeed:
                    return addSpeedSprite;
                case PowerUpType.InvertAxis:
                    return addInvertAxisSprite;
                case PowerUpType.BombKick:
                    return bombKickSprite;
                default: return null;
            }
       }
    }
}