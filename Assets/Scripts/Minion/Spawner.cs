using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] float spawnTimer;
    float spawnCooldown = 0;

    void Start()
    {
        player = PlayerStats.instance.gameObject;
    }

    void Update()
    {
        spawnCooldown += Time.deltaTime;
        if (spawnCooldown > spawnTimer)
        {
            Debug.Log("Enemy Spawned");
            Instantiate(enemy, transform.position, enemy.transform.rotation);
            enemy.layer = 9;
            spawnCooldown = 0;
        }
    }
}
