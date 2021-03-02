using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Primozov.AmongBombs
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] GameObject explosionPrefab;
        [SerializeField] LayerMask raycastFilterLayer;

        private CinemachineImpulseSource impulseSource;

        private float explosionTime = 0;
        private int bombRange;
        private Collider2D mCollider;
        private Rigidbody2D rb;
        GroundTile groundTile;

        private bool exploded = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public void SetupBomb(int bombRange, float secondsToExplode)
        {
            explosionTime = Time.time + secondsToExplode;
            this.bombRange = bombRange;
        }

        private void Start()
        {
            mCollider = GetComponent<Collider2D>();
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        void Update()
        {
            if (Time.time > explosionTime)
            {
                Explode();
            }
        }

        public void Explode()
        {
            if (!exploded)
            {
                StopMoving();
                impulseSource.m_ImpulseDefinition.m_AmplitudeGain += bombRange * 0.1f;
                impulseSource.GenerateImpulse();
                exploded = true;
                Explosion explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<Explosion>();
                explosion.Setup(true, Explosion.ExplosionDirection.ALL, bombRange, groundTile);
                Destroy(gameObject);
            }
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
            rb.velocity = Vector2.zero;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, raycastFilterLayer);
            if (hit.collider != null && hit.collider.CompareTag("Ground"))
            {
                groundTile = hit.collider.gameObject.GetComponent<GroundTile>();
                transform.position = groundTile.transform.position;
            }
        }
    }
}
