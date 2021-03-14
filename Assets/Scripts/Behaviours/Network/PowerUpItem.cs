using UnityEngine;
using Mirror;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class PowerUpItem : NetworkBehaviour
    {
        public enum PowerUpType
        {
            AddBomb, AddRange, PowerBomb, AddSpeed, InvertAxis, BombKick
        }

        [SerializeField] PowerUpType powerUpType;
        [SerializeField] Sprite sprite;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] private float secondsInvertedAxis = 5;

        public void SetPowerUpType(PowerUpType powerUpType)
        {
            this.powerUpType = powerUpType;
        }

        void Start()
        {
            //spriteRenderer.sprite = sprite;
        }

        public void Collect(GameObject playerGameObject)
        {
            if (isLocalPlayer)
            {
                switch (powerUpType)
                {
                    case PowerUpType.AddBomb:
                        playerGameObject.GetComponent<BombDropper>().IncreaseBombs();
                        break;
                    case PowerUpType.AddRange:
                        playerGameObject.GetComponent<BombDropper>().IncreaseRange();
                        break;
                    case PowerUpType.PowerBomb:
                        playerGameObject.GetComponent<BombDropper>().IncreaseToMaxRange();
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
}
