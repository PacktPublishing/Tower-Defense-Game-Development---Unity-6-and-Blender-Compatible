using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class LevelManager : MonoBehaviour {

    GameObject[] spawnPoints;
    static int totalEnemies = 0;

    public ParticleSystem deathParticlePrefab;
    public static IObjectPool<ParticleSystem> deathParticlePool;

    void Start()
    {

        spawnPoints = GameObject.FindGameObjectsWithTag("spawn");

        foreach (GameObject sp in spawnPoints)
        {

            totalEnemies += sp.GetComponent<Spawn>().maxCount;
        }
        deathParticlePool = new ObjectPool<ParticleSystem>(CreateDeathExplosion, OnTakeFromPool, OnReturnedToPool, null, true, 10, 20);
    }

    ParticleSystem CreateDeathExplosion()
    {

        ParticleSystem particleSystem = Instantiate(deathParticlePrefab);
        particleSystem.Stop();
        return particleSystem;
    }

    void OnReturnedToPool(ParticleSystem system)
    {

        system.gameObject.SetActive(false);
    }

    void OnTakeFromPool(ParticleSystem system)
    {

        system.gameObject.SetActive(true);
    }

    public static void DisplayDeathExplosion(Vector3 position)
    {


        ParticleSystem deathExp = deathParticlePool.Get();
        if (deathExp)
        {

            deathExp.transform.position = position;
            deathExp.Play();
        }
    }

    public static void RemoveEnemy()
    {

        totalEnemies--;

        if (totalEnemies <= 0)
        {

            Debug.Log("Level Over");
        }
    }


    void Update()
    {

    }
}
