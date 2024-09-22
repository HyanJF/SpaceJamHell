using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform player;
    public Rigidbody bullet;
    public Transform spawner;
    public float bulletSpeed;
    bool isShooting = false;

    void Start()
    {
        StartCoroutine(shoot());
        player = PlayerStats.instance.transform;
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 distance = this.transform.position - player.transform.position;
        if (Physics.Linecast(this.transform.position, player.transform.position, out hit, -1))
        {
            if (hit.transform.CompareTag("Player"))
            {
                StartCoroutine(enemyLife());
                if (distance.magnitude > 10) // Enemy's range
                {
                    //StartCoroutine(shoot()); AntiLarry
                    Debug.Log("Enemy Approaching");
                    this.transform.Translate(Vector3.forward * 2f * Time.deltaTime);
                    this.transform.LookAt(player.transform);
                    isShooting = false;
                }
                else
                {
                    isShooting = true;
                    this.transform.LookAt(player.transform);
                }
            }
        }
    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds(0.5f);
        if (isShooting)
        {
            Debug.Log("Enemy Shooting");
            Rigidbody clone;
            clone = (Rigidbody)Instantiate(bullet, spawner.transform.position, Quaternion.identity);
            clone.velocity = spawner.TransformDirection(Vector3.forward * bulletSpeed * Time.deltaTime);
        }
        StartCoroutine(shoot());
    }

    IEnumerator enemyLife()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Destroying Enemy");
        Destroy(gameObject);
    }
}
