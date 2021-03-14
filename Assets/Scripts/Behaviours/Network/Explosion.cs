using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using System.Collections.Generic;
using Primozov.AmongBombs.Behaviours.Mono;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class Explosion : NetworkBehaviour
    {
        [SerializeField] GameObject explosionPrefab;
        [SerializeField] LayerMask filterRaycastLayers;

        [SerializeField] UnityEvent onSourceExplosion;

        public enum ExplosionDirection
        {
            Up, Down, Right, Left, ALL
        }

        [Server]
        private void CheckHitSomething()
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.4f, Vector2.zero, 0.5f, filterRaycastLayers);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Bomb"))
                {                    
                    StartCoroutine(ChainExplosion(hit.collider.gameObject));
                }
                else if (hit.collider.CompareTag("Player"))
                {
                    DamagePlayerOnClients(hit.collider.gameObject);
                }
            }
        }

        IEnumerator ChainExplosion(GameObject obj) {
            yield return new WaitForSeconds(0.2f);
            if (obj != null)
            {
                Bomb bomb = obj.GetComponent<Bomb>();
                bomb.Explode();
            }
           
        }

        [ClientRpc]
        public void DamagePlayerOnClients(GameObject obj)
        {
            Damagable damagable = obj.GetComponent<Damagable>();
            damagable.TakeDamage(0);
        }

        [Server]
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
            StartCoroutine(DestroyGameObjectOnClients(gameObject, 1f));
        }

        public IEnumerator DestroyGameObjectOnClients(GameObject obj, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            NetworkServer.Destroy(obj);
        }

        [Server]
        private void SpreadExplosion(GroundTile.NeighborInfo neighborInfo, ExplosionDirection direction, int bombRange)
        {
            switch (neighborInfo.neighborType)
            {
                case GroundTile.NeighborType.Destructable:
                    //Destroy the Destructable
                    Loottable loottable  = neighborInfo.gameObject.GetComponent<Loottable>();
                    loottable.Loot();
                    StartCoroutine(DestroyGameObjectOnClients(neighborInfo.gameObject, 0f));
                    break;
                case GroundTile.NeighborType.Indestructable:
                    //Nothing to do
                    break;
                case GroundTile.NeighborType.Empty:
                    //Spawn Explosion
                    GameObject explosionObject = Instantiate(explosionPrefab, neighborInfo.gameObject.transform.position, Quaternion.identity);
                    Explosion explosion = explosionObject.GetComponent<Explosion>();
                    explosion.Setup(false, direction, bombRange, neighborInfo.gameObject.GetComponent<GroundTile>());
                    NetworkServer.Spawn(explosionObject);
                    break;
            }
        }
    }
}
