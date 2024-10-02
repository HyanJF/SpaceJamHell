using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] float spawnTimer;
    public GameObject[] spawners;
    float spawnCooldown = 0;

    public List<AudioClip> audioClips;
    public AudioSource audioSource;

    void Start()
    {
        player = PlayerStats.instance.gameObject;
    }

    void Update()
    {
        int r = Random.Range(0, audioClips.Count);
        spawnCooldown += Time.deltaTime;
        if (spawnCooldown > spawnTimer)
        {
            Debug.Log("Enemy Spawned");
            Instantiate(enemy, spawners[Random.Range(0,spawners.Length)].transform.position, enemy.transform.rotation);
            AudioClip clip = audioClips[r];
            audioSource.clip = clip;
            audioSource.Play();
            enemy.layer = 9;
            spawnCooldown = 0;
        }
    }
}
