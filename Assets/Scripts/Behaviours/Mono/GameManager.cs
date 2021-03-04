using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Primozov.AmongBombs
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameObject destructables;
        [SerializeField] int minEmptyBlocksCount;
        [SerializeField] int maxEmptyBlocksCount;

        void Start()
        {
            int desctructableCount = destructables.transform.childCount;

            if (maxEmptyBlocksCount > desctructableCount)
                maxEmptyBlocksCount = desctructableCount;


            int neededEptyBlocks = Random.Range(minEmptyBlocksCount, maxEmptyBlocksCount);

            while(neededEptyBlocks > 0)
            {
                int randomIndex = Random.Range(0, destructables.transform.childCount);
                GameObject child = destructables.transform.GetChild(randomIndex).gameObject;
                if (child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(false);
                    neededEptyBlocks--;
                }
            }
        }
    }
}
