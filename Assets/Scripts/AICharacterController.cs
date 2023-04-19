using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


    public class AICharacterController : MonoBehaviour
    {
        public float moveSpeed = 3f;
        public float detectionRange = 10f;
        public Transform target;
        public Transform[] spawnPoints;
        public GameObject aiCharacterPrefab;
        public float spawnDelay = 5f;

        private NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            StartCoroutine(SpawnCharacters());
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, target.position) < detectionRange)
            {
                navMeshAgent.SetDestination(target.position);
            }
        }

        private IEnumerator SpawnCharacters()
        {
            List<int> usedSpawnPointIndices = new List<int>(); // keep track of which spawn points have already been used

            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);

                // find an unused spawn point
                int spawnPointIndex;
                do
                {
                    spawnPointIndex = Random.Range(0, spawnPoints.Length);
                } while (usedSpawnPointIndices.Contains(spawnPointIndex));

                // spawn a character at the selected spawn point
                Transform spawnPoint = spawnPoints[spawnPointIndex];
                Instantiate(aiCharacterPrefab, spawnPoint.position, spawnPoint.rotation);

                // mark the spawn point as used
                usedSpawnPointIndices.Add(spawnPointIndex);

                // if all spawn points have been used, reset the list
                if (usedSpawnPointIndices.Count == spawnPoints.Length)
                {
                    usedSpawnPointIndices.Clear();
                }
            }
        }
    }
