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
        [SerializeField] int secondsToDraw;

        int playersAlive;

        [Server]
        void Start()
        {
            RandomizeBlocks();
            ResetRound();
        }

        [Server]
        private void ResetRound()
        {
            playersAlive = FindObjectOfType<NetworkRoomManagerCustom>().numPlayers;  
        }

        public void PlayerDied()
        {
            playersAlive--;
            StartCoroutine(CheckWinner());
        }

        private IEnumerator CheckWinner()
        {
            yield return new WaitForSeconds(secondsToDraw);
            if (playersAlive == 1)
            {
                //TODO: Show Winner
                Debug.Log("WINNER");
            }
            else if (playersAlive == 0) {
                //TODO: Draw Game
                Debug.Log("DRAW");
            }
        }

        [Server]
        private void RandomizeBlocks()
        {
            int desctructableCount = destructables.transform.childCount;

            if (maxEmptyBlocksCount > desctructableCount)
                maxEmptyBlocksCount = desctructableCount;


            int neededEptyBlocks = Random.Range(minEmptyBlocksCount, maxEmptyBlocksCount);

            while (neededEptyBlocks > 0)
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

        [Server]
        private void Update()
        {
            
        }

        [ClientRpc]
        public void DisableBlock(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
