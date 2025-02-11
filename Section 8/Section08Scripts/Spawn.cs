using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public GameObject enemyPrefab;
    public Transform homeLocation;
    public float startDelay = 1.0f;
    public float spawnRate = 0.3f;
    public int maxCount = 10;
    int count = 0;

    void Start() {

        InvokeRepeating("Spawner", startDelay, spawnRate);
    }

    void Spawner() {

        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<FindHome>().destination = homeLocation;
        count++;
        if (count >= maxCount) CancelInvoke("Spawner");
    }
}
