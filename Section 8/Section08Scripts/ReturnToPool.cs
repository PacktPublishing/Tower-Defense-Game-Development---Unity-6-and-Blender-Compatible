using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool : MonoBehaviour {

    public ParticleSystem system;
    public AudioSource audioSource;
    public IObjectPool<ParticleSystem> pool;

    void Start()
    {

        var main = system.main;
        main.stopAction = ParticleSystemStopAction.Callback;
        pool = LevelManager.deathParticlePool;
    }

    void OnParticleSystemStopped()
    {

        pool.Release(system);
    }

    private void OnEnable()
    {

        audioSource.Play();
    }

    void Update()
    {

    }
}
