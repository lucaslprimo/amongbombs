using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Primozov.AmongBombs
{
    public class SpawnPoints : MonoBehaviour
    {
        private List<Transform> spawnPoints = new List<Transform>();
        private int spawnIndex = 0;

        private void Start()
        {
            for(int i=0; i<transform.childCount; i++)
            {
                spawnPoints.Add(transform.GetChild(i));
            }
        }

        public Transform GetNextSpawnPoint()
        {
            Transform nextSpawnPoint = spawnPoints[spawnIndex];

            spawnIndex++;
            if(spawnIndex == transform.childCount)
            {
                spawnIndex = 0;
            }

            return nextSpawnPoint;
        }


    }
}