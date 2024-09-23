using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private LayerMask willAttack;
    [SerializeField] float waveTimer;
    [SerializeField] float waveSpawn;
    [SerializeField] float detectionDistance;
    [SerializeField] GameObject player;

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
        waveTimer += Timer();
        if (waveTimer > waveSpawn)
        {
            StartCoroutine(WaveAttack());
            waveTimer = 0;
        }
    }

    private IEnumerator WaveAttack()
    {
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
            if (c.GetComponent<PlayerManager>())
            {
                Debug.Log("Applying Knockback");
                c.GetComponent<PlayerManager>().ApplyKnockback();
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
