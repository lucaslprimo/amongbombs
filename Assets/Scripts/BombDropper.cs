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
       
        private List<float> bombDropCooldownTimes = new List<float>();
        private int bombRange = 0;
        private int bombIndex = 0;

        private void Start()
        {
            IncreaseMaxBombs();
            IncreaseMaxBombs();
            IncreaseMaxBombs();
            IncreaseRange();
            IncreaseRange();
            IncreaseRange();
        }

        public void IncreaseRange()
        {
            bombRange++;
        }

        public void IncreaseMaxBombs()
        {
            bombDropCooldownTimes.Add(0);
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
