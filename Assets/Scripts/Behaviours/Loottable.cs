using Unity.Netcode;
using UnityEngine;

namespace Primozov.AmongBombs.Behaviours.Mono
{
    public class Loottable : NetworkBehaviour
    {
        [Range(0.0f, 100.0f)]
        [SerializeField] float lootChance;

        [SerializeField] GameObject itemPrefab;
        [SerializeField] PowerUpType[] lootItems;

        [Rpc(SendTo.Server)]
        public void LootRpc()
        {
            float randomNumber = Random.Range(0, 100);

            if(lootChance > randomNumber)
            {
                DropItemRpc();
            }
        }

        [Rpc(SendTo.Server)]
        private void DropItemRpc()
        {
            PowerUpItem powerUp = Instantiate(itemPrefab, transform.position, Quaternion.identity).GetComponent<PowerUpItem>();
            powerUp.SetPowerUpType(lootItems[Random.Range(0,lootItems.Length)]);
        }
    }
}
