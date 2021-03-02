using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Primozov.AmongBombs
{
    public class GroundTile : MonoBehaviour
    {
        [SerializeField] Transform leftPoint;
        [SerializeField] Transform rightPoint;
        [SerializeField] Transform topPoint;
        [SerializeField] Transform bottomPoint;
        [SerializeField] LayerMask filterMaskLayer;

        private float raycastSize = 0.1f;

        public enum NeighborType
        {
            Destructable,
            Indestructable,
            Empty
        }

        public struct NeighborInfo
        {
            public GameObject gameObject;
            public NeighborType neighborType;
        }

        public NeighborInfo GetLeftNeighbor()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(leftPoint.position, Vector2.left, raycastSize, filterMaskLayer);
            return GetPriorityNeighborInfo(hits);
        }

        public NeighborInfo GetRightNeighbor()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(rightPoint.position, Vector2.right, raycastSize, filterMaskLayer);
            return GetPriorityNeighborInfo(hits);
        }

        public NeighborInfo GetTopNeighbor()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(topPoint.position, Vector2.up, raycastSize, filterMaskLayer);
            return GetPriorityNeighborInfo(hits);
        }

        public NeighborInfo GetBottomNeighbor()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(bottomPoint.position, Vector2.down, raycastSize, filterMaskLayer);
            return GetPriorityNeighborInfo(hits);
        }

        private NeighborInfo GetPriorityNeighborInfo(RaycastHit2D[] hits)
        {
            NeighborInfo neighborInfo = new NeighborInfo();

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    NeighborInfo neighborInfoTemp = new NeighborInfo();
                    neighborInfoTemp.neighborType = GetNeighborTypeFromCollider(hit.collider);
                    neighborInfoTemp.gameObject = hit.collider.gameObject;

                    if (neighborInfo.gameObject == null)
                    {
                        neighborInfo = neighborInfoTemp;
                    }
                    else if (neighborInfoTemp.neighborType == NeighborType.Destructable)
                    {
                        neighborInfo = neighborInfoTemp;
                    }
                }
            }
            return neighborInfo;
        }

        private NeighborType GetNeighborTypeFromCollider(Collider2D collider)
        {
            if (collider.CompareTag("Ground"))
            {
                return NeighborType.Empty;
            }
            else if (collider.CompareTag("Destructable"))
            {
                return NeighborType.Destructable;
            }
            else if (collider.CompareTag("Indestructable"))
            {
                return NeighborType.Indestructable;
            }

            return NeighborType.Empty;
        }
    }
}
