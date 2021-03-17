using Primozov.AmongBombs.Systems;
using UnityEngine;
using Mirror;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class BombDropper : NetworkBehaviour
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
            if (isLocalPlayer)
            {
                if (Input.GetKeyDown(key))
                {
                    TryBombDrop();
                }
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
                    CmdDropBomb(hit.collider.transform.position);
                    bombDropSystem.BombDroped(Time.time);
                }
            }
        }

        [Command]
        private void CmdDropBomb(Vector3 position)
        {
            GameObject bombObject = (GameObject)Instantiate(bombPrefab, position, Quaternion.identity);
            Bomb bomb = bombObject.GetComponent<Bomb>();
            bomb.SetupBomb(bombDropSystem.GetBombRange(), secondsToExplode);
            NetworkServer.Spawn(bombObject);
        }
    }
}