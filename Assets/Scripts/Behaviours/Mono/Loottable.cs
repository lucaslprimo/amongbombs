using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Primozov.AmongBombs
{
    public class Loottable : MonoBehaviour
    {
        [Range(0.0f, 100.0f)]
        [SerializeField] float lootChance;

        [SerializeField] GameObject itemPrefab;
        [SerializeField] PowerUpItem.PowerUpType[] lootItems;

        public void Loot()
        {
            float randomNumber = Random.Range(0, 100);

            if(lootChance > randomNumber)
            {
                DropItem();
            }
        }

        private void DropItem()
        {
            PowerUpItem powerUp = Instantiate(itemPrefab, transform.position, Quaternion.identity).GetComponent<PowerUpItem>();
            powerUp.SetPowerUpType(lootItems[Random.Range(0,lootItems.Length)]);
        }
    }
}
