using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs = new GameObject[3];
    public Vector3 spawnPos;

    public float startDelay;
    public Vector2 repeatRate;

    private PlayerController playerControllerScript;

    private void Start()
    {
        Invoke("SpawnObstacle", startDelay);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void SpawnObstacle()
    {
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);

        float timeInterval = Random.Range(repeatRate.x, repeatRate.y);
        if (!playerControllerScript.gameOver)
        {
            Invoke("SpawnObstacle", timeInterval);
        }
    }
}
