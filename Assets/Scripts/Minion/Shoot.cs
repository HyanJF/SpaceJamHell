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
    public bool antiLarry = false;

    void Start()
    {
        if (!antiLarry)
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
                if (distance.magnitude > 10 && antiLarry) // Enemy's range
                {
                    Debug.Log("Enemy Approaching");
                    this.transform.Translate(Vector3.forward * 5f * Time.deltaTime);
                    this.transform.LookAt(player.transform);
                    isShooting = false;
                }
                else if (distance.magnitude < 10 && !antiLarry)
                {
                    Debug.Log("Enemy Approaching");
                    this.transform.Translate(Vector3.forward * 5f * Time.deltaTime);
                    this.transform.LookAt(player.transform);
                    isShooting = true;
                }
                else
                {
                    isShooting = true;
                    this.transform.LookAt(player.transform);
                    if (antiLarry)
                        StartCoroutine(shoot());
                }
            }
        }
    }

    IEnumerator shoot()
    {

        if (antiLarry) yield return new WaitForSeconds(1f);
        else yield return new WaitForSeconds(0.5f);
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
        yield return new WaitForSeconds(5f);
        Debug.Log("Destroying Enemy");
        Destroy(gameObject);
    }
}
