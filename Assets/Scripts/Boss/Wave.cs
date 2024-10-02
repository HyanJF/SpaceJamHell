using System.Collections;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private LayerMask willAttack;
    [SerializeField] float waveTimer;
    [SerializeField] float waveSpawn;
    [SerializeField] float detectionDistance;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] minionSpawners;
    private bool secondStage;
    private bool thirdStage;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerStats.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Life.instance.health == 2)
        {
            Debug.Log("Commencing second Stage");
            Debug.Log("Spawning Defensive Minions");
            secondStage = true;
            minionSpawners[1].SetActive(true);
        }
        else if (Life.instance.health == 1)
        {
            Debug.Log("Commencing third Stage");
            Debug.Log("Spawning Anti-Larrys");
            secondStage = false;
            thirdStage = true;
            minionSpawners[1].SetActive(false);
            minionSpawners[0].SetActive(true);
        }
        waveTimer += Timer();
        if (waveTimer > waveSpawn || waveTimer > waveSpawn - 1 && secondStage || waveTimer > waveSpawn - 1.5f && thirdStage)
        {
            StartCoroutine(WaveAttack());
            waveTimer = 0;
        }
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > detectionDistance) waveTimer = 0;
    }

    private IEnumerator WaveAttack()
    {
        if (Life.instance.health == 3)
            yield return new WaitForSeconds(waveSpawn - 1.2f);
        else if (Life.instance.health == 2)
            yield return new WaitForSeconds(waveSpawn - 0.8f);
        else if (Life.instance.health == 1)
            yield return new WaitForSeconds(waveSpawn);
        Debug.Log("Commencing Defensive Wave");
        ShockWave();
    }

    private void ShockWave()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, detectionDistance, willAttack);
        Debug.Log("Player Detected");
        foreach(Collider c in collider)
        {
            if (c.GetComponent<PlayerV2Manager>())
            {
                Debug.Log("Applying Knockback");
                if (Life.instance.health == 3) c.GetComponent<PlayerV2Manager>().ApplyKnockback(50);
                else if (Life.instance.health == 2) c.GetComponent<PlayerV2Manager>().ApplyKnockback(100);
                else if (Life.instance.health == 1) c.GetComponent<PlayerV2Manager>().ApplyKnockback(250);
            }
        }
    }

    float Timer()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < detectionDistance)
            {
                Debug.Log("Timer increased");
                // StartCoroutine(WaveAttack());
                return Time.deltaTime;
            }
        }
        return 0;
    }
}
