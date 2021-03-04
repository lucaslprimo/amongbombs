using Primozov.AmongBombs.Behaviours.Mono;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Primozov.AmongBombs
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] GameObject explosionPrefab;
        [SerializeField] LayerMask filterRaycastLayers;

        [SerializeField] UnityEvent onSourceExplosion;

        public enum ExplosionDirection
        {
            Up, Down, Right, Left, ALL
        }

        private void CheckHitSomething()
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.4f, Vector2.zero, 0.5f, filterRaycastLayers);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Bomb"))
                {
                    Bomb bomb = hit.collider.gameObject.GetComponent<Bomb>();
                    StartCoroutine(ChainExplosion(bomb));
                }
                else if (hit.collider.CompareTag("Player"))
                {
                    Damagable damagable = hit.collider.gameObject.GetComponent<Damagable>();
                    damagable.TakeDamage(0);
                }
            }
        }

        IEnumerator ChainExplosion(Bomb bomb) {
            yield return new WaitForSeconds(0.2f);
            bomb.Explode();
        }

        public void Setup(bool isSource, ExplosionDirection direction, int bombRange, GroundTile groundTile)
        {
            CheckHitSomething();
            if (isSource)
            {
                onSourceExplosion.Invoke();
                bombRange--;
                SpreadExplosion(groundTile.GetBottomNeighbor(), ExplosionDirection.Down, bombRange);
                SpreadExplosion(groundTile.GetTopNeighbor(), ExplosionDirection.Up, bombRange);
                SpreadExplosion(groundTile.GetLeftNeighbor(), ExplosionDirection.Left, bombRange);
                SpreadExplosion(groundTile.GetRightNeighbor(), ExplosionDirection.Right, bombRange);
            }
            else
            {
                if (bombRange > 0)
                {
                    bombRange--;
                    switch (direction)
                    {
                        case ExplosionDirection.Up:
                            SpreadExplosion(groundTile.GetTopNeighbor(), direction, bombRange);
                            break;
                        case ExplosionDirection.Down:
                            SpreadExplosion(groundTile.GetBottomNeighbor(), direction, bombRange);
                            break;
                        case ExplosionDirection.Right:
                            SpreadExplosion(groundTile.GetRightNeighbor(), direction, bombRange);
                            break;
                        case ExplosionDirection.Left:
                            SpreadExplosion(groundTile.GetLeftNeighbor(), direction, bombRange);
                            break;
                    }
                }
                else
                {
                    //Explosion Tail
                }
            }
            Destroy(gameObject, 1f);
        }

        private void SpreadExplosion(GroundTile.NeighborInfo neighborInfo, ExplosionDirection direction, int bombRange)
        {
            switch (neighborInfo.neighborType)
            {
                case GroundTile.NeighborType.Destructable:
                    //Destroy the Destructable
                    Loottable loottable = neighborInfo.gameObject.GetComponent<Loottable>();
                    loottable.Loot();
                    Destroy(neighborInfo.gameObject);
                    break;
                case GroundTile.NeighborType.Indestructable:
                    //Nothing to do
                    break;
                case GroundTile.NeighborType.Empty:
                    //Spawn Explosion
                    Explosion explosion = Instantiate(explosionPrefab, neighborInfo.gameObject.transform.position, Quaternion.identity).GetComponent<Explosion>();
                    explosion.Setup(false, direction, bombRange, neighborInfo.gameObject.GetComponent<GroundTile>());
                    break;
            }
        }
    }
}
