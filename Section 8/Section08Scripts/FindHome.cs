using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class FindHome : MonoBehaviour {

    public Transform destination;
    public EnemyDetails enemyDetails;
    public Slider healthBarPrefab;
    NavMeshAgent ai;
    int currentHealth;
    Slider healthBar;

    void Start()
    {

        ai = GetComponent<NavMeshAgent>();
        ai.SetDestination(destination.position);
        ai.speed = enemyDetails.speed;
        currentHealth = enemyDetails.maxHealth;
        healthBar = Instantiate(healthBarPrefab, this.transform.position, Quaternion.identity);
        healthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        healthBar.value = healthBar.maxValue = enemyDetails.maxHealth;
    }

    public void Hit(int power)
    {

        if (healthBar)
        {

            healthBar.value -= power;
            if (healthBar.value <= 0)
            {

                LevelManager.DisplayDeathExplosion(this.transform.position + new Vector3(0.0f, 0.5f, 0.0f));
                Destroy(healthBar.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

    void Update()
    {

        if (ai.remainingDistance < 0.5f && ai.hasPath)
        {

            LevelManager.RemoveEnemy();
            ai.ResetPath();
            Destroy(healthBar.gameObject);
            Destroy(this.gameObject, 0.1f);
        }

        if (healthBar)
        {

            healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up * 1.2f);
        }
    }
}
