using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
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
