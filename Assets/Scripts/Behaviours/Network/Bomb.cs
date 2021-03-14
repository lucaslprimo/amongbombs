using Cinemachine;
using Mirror;
using Primozov.AmongBombs.Systems;
using UnityEngine;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class Bomb : NetworkBehaviour
    {
        [SerializeField] GameObject explosionPrefab;
        [SerializeField] LayerMask raycastFilterLayer;

        private CinemachineImpulseSource impulseSource;
        private Collider2D mCollider;
        private Rigidbody2D rb;
        GroundTile groundTile;

        BombSystem bombSystem;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        [Server]
        public void SetupBomb(int bombRange, float secondsToExplode)
        {
            bombSystem = new BombSystem(bombRange, secondsToExplode);
            bombSystem.onExplode += Explode;
        }

        private void Start()
        {
            mCollider = GetComponent<Collider2D>();
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        [Server]
        void Update()
        {
            bombSystem.Update(Time.time);
        }

        [Server]
        public void Explode()
        {
            StopMoving();
            ShakeCameraClients(bombSystem.GetBombRange());
            GameObject explosionObject = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Explosion explosion = explosionObject.GetComponent<Explosion>();
            NetworkServer.Spawn(explosionObject);
            explosion.Setup(true, Explosion.ExplosionDirection.ALL, bombSystem.GetBombRange(), groundTile);
            NetworkServer.Destroy(gameObject);
        }

        [ClientRpc]
        public void ShakeCameraClients(float bombRange)
        {
            impulseSource.m_ImpulseDefinition.m_AmplitudeGain += bombRange;
            impulseSource.GenerateImpulse();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            mCollider.isTrigger = false;
        }

        [Server]
        private void OnCollisionEnter2D(Collision2D collision)
        {
            StopMoving();
        }

        [Server]
        private void StopMoving()
        {
            if (rb)
            {
                rb.velocity = Vector2.zero;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, raycastFilterLayer);
                if (hit.collider != null && hit.collider.CompareTag("Ground"))
                {
                    groundTile = hit.collider.gameObject.GetComponent<GroundTile>();
                    OnHitUpdateClients(hit.collider.gameObject);
                }
            }
        }

        [ClientRpc]
        private void OnHitUpdateClients(GameObject obj)
        {
            GroundTile groundTile = obj.GetComponent<GroundTile>();
            transform.position = groundTile.transform.position;
        }
    }
}
