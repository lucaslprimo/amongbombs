using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Primozov.AmongBombs
{
    public class BombDropper : MonoBehaviour
    {
        [SerializeField] KeyCode key;
        [SerializeField] GameObject bombPrefab;
        [SerializeField] float secondsToExplode = 3;
        [SerializeField] LayerMask raycastFilterLayer;
        [SerializeField] int bombLimit = 10;
        [SerializeField] int rangeLimit = 10;

        private List<float> bombDropCooldownTimes = new List<float>();
        private int bombRange = 0;
        private int bombIndex = 0;

        private void Start()
        {
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

        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                TryBombDrop();
            }
        }

        private void TryBombDrop()
        {
            if (Time.time > bombDropCooldownTimes[bombIndex])
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, raycastFilterLayer);
                if (hit.collider != null && hit.collider.CompareTag("Ground"))
                {
                    GroundTile groundTile = hit.collider.gameObject.GetComponent<GroundTile>();
                    DropBomb(hit.collider.transform.position, groundTile);
                    bombDropCooldownTimes[bombIndex] = Time.time + secondsToExplode;
                    bombIndex++;

                    if (bombIndex == bombDropCooldownTimes.Count)
                    {
                        bombIndex = 0;
                    }
                }
            }
        }

        private void DropBomb(Vector3 position, GroundTile groundTile)
        {
            Bomb bomb = Instantiate(bombPrefab, position, Quaternion.identity).GetComponent<Bomb>();
            bomb.SetupBomb(bombRange, secondsToExplode);
        }
    }
}
