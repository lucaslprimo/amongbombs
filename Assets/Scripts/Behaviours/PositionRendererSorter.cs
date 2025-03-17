using Unity.Netcode;
using UnityEngine;

public class PositionRendererSorter : NetworkBehaviour
{
    [SerializeField] int sortingOrderbase = 5000;
    [SerializeField] int offset;
    Renderer mRenderer;

    void Start()
    {
        mRenderer = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        mRenderer.sortingOrder = (int)(sortingOrderbase - transform.position.y - offset);
    }
}
