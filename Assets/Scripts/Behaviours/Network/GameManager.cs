using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Primozov.AmongBombs.Behaviours.Network
{
    public class GameManager : NetworkBehaviour
    {
        [SerializeField] GameObject destructables;
        [SerializeField] int minEmptyBlocksCount;
        [SerializeField] int maxEmptyBlocksCount;

        [Server]
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
                    DisableBlock(child.gameObject);
                    neededEptyBlocks--;
                }
            }
        }

        [ClientRpc]
        public void DisableBlock(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
