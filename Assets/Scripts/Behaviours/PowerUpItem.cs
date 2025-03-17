using Unity.Netcode;
using UnityEngine;

namespace Primozov.AmongBombs.Behaviours.Mono
{
    public class PowerUpItem : NetworkBehaviour
    {
        [SerializeField] PowerUpType powerUpType;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] private float secondsInvertedAxis = 5;
        [SerializeField] PowerUpItemSprites spritesObject;

        public void SetPowerUpType(PowerUpType powerUpType)
        {
            this.powerUpType = powerUpType;
            spriteRenderer.sprite = spritesObject.GetSpriteByType(powerUpType);
        }

        public void Collect(GameObject playerGameObject)
        {
            switch (powerUpType)
            {
                case PowerUpType.AddBomb:
                    playerGameObject.GetComponent<BombDropper>().IncreaseBombs();
                    break;
                case PowerUpType.AddRange:
                    playerGameObject.GetComponent<BombDropper>().IncreaseRange();
                    break;
                case PowerUpType.AddSpeed:
                    playerGameObject.GetComponent<PlayerMovement>().IncreaseSpeed();
                    break;
                case PowerUpType.InvertAxis:
                    playerGameObject.GetComponent<PlayerMovement>().InvertAxis(secondsInvertedAxis);
                    break;
                case PowerUpType.BombKick:
                    playerGameObject.GetComponent<BombKick>().enabled = true;
                    break;
            }
        }
    }
}
