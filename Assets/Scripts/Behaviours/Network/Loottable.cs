using UnityEngine;
using Mirror;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class Loottable : NetworkBehaviour
    {
        [Range(0.0f, 100.0f)]
        [SerializeField] float lootChance;

        [SerializeField] GameObject itemPrefab;
        [SerializeField] PowerUpItem.PowerUpType[] lootItems;

        [Server]
        public void Loot()
        {
            float randomNumber = Random.Range(0, 100);

            if(lootChance > randomNumber)
            {
                DropItem();
            }
        }

        [Server]
        private void DropItem()
        {
            GameObject powerUpItemObject = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            PowerUpItem powerUp = powerUpItemObject.GetComponent<PowerUpItem>();
            powerUp.SetPowerUpType(lootItems[Random.Range(0,lootItems.Length)]);
            NetworkServer.Spawn(powerUpItemObject);
        }
    }
}
