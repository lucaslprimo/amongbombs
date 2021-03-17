using UnityEngine;
using Mirror;
using System.Collections;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class PowerUpItem : NetworkBehaviour
    {
        [SerializeField] PowerUpType powerUpType;
        [SerializeField] public SpriteRenderer spriteRenderer;
        [SerializeField] public BoxCollider2D mCollider;
        [SerializeField] private float secondsInvertedAxis = 5;
        [SerializeField] PowerUpItemSprites spritesObject;

        public void SetPowerUpType(PowerUpType powerUpType)
        {
            this.powerUpType = powerUpType;
            spriteRenderer.sprite = spritesObject.GetSpriteByType(powerUpType);
        }

        public void Collect(GameObject playerGameObject)
        {
            PlayerNetwork.Instance.CmdDisableItemOnClients(this);
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

            StartCoroutine(DestroyAfterSeconds(gameObject, 1f));
        }

        public IEnumerator DestroyAfterSeconds(GameObject obj, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            PlayerNetwork.Instance.CmdDestroyItem(obj);
        }

        public void PlayPickUpSound()
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
