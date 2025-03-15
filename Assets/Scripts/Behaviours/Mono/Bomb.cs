using Cinemachine;
using Primozov.AmongBombs.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Primozov.AmongBombs.Behaviours.Mono
{
    public class Bomb : MonoBehaviour
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

        void Update()
        {
            bombSystem.Update(Time.time);
        }

        public void Explode()
        {
            StopMoving();
            impulseSource.m_ImpulseDefinition.m_AmplitudeGain += bombSystem.GetBombRange();
            impulseSource.GenerateImpulse();
            Explosion explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<Explosion>();
            explosion.Setup(true, Explosion.ExplosionDirection.ALL, bombSystem.GetBombRange(), groundTile);
            Destroy(gameObject);   
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            mCollider.isTrigger = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            StopMoving();
        }

        private void StopMoving()
        {
            if (rb)
            {
                rb.linearVelocity = Vector2.zero;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, raycastFilterLayer);
                if (hit.collider != null && hit.collider.CompareTag("Ground"))
                {
                    groundTile = hit.collider.gameObject.GetComponent<GroundTile>();
                    transform.position = groundTile.transform.position;
                }
            }
          
        }
    }
}
