using Primozov.AmongBombs.Systems;
using UnityEngine;

namespace Primozov.AmongBombs.Behaviours.Mono
{
    public class BombDropper : MonoBehaviour
    {
        [SerializeField] KeyCode key;
        [SerializeField] GameObject bombPrefab;
        [SerializeField] float secondsToExplode = 3;
        [SerializeField] LayerMask raycastFilterLayer;
        [SerializeField] int bombLimit = 10;
        [SerializeField] int rangeLimit = 13;

        private BombDropSystem bombDropSystem;

        private void Start()
        {
            bombDropSystem = new BombDropSystem(secondsToExplode, bombLimit, rangeLimit);
        }

        void Update()
        { 
            if (Input.GetKeyDown(key))
            {
                TryBombDrop();
            }
        }

        public void IncreaseRange()
        {
            bombDropSystem.IncreaseRange();
        }

        public void IncreaseBombs()
        {
            bombDropSystem.IncreaseBombs();
        }

        public void IncreaseToMaxRange()
        {
            bombDropSystem.IncreaseToMaxRange();
        }

        public void IncreaseToMaxBombs()
        {
            bombDropSystem.IncreaseToMaxBombs();
        }

        private void TryBombDrop()
        {
            if (bombDropSystem.CanDropBomb(Time.time))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, raycastFilterLayer);
                if (hit.collider != null && hit.collider.CompareTag("Ground"))
                {
                    GroundTile groundTile = hit.collider.gameObject.GetComponent<GroundTile>();
                    DropBomb(hit.collider.transform.position, groundTile);
                    bombDropSystem.BombDroped(Time.time);
                }
            }
        }

        private void DropBomb(Vector3 position, GroundTile groundTile)
        {
            Bomb bomb = Instantiate(bombPrefab, position, Quaternion.identity).GetComponent<Bomb>();
            bomb.SetupBomb(bombDropSystem.GetBombRange(), secondsToExplode);
        }
    }
}
