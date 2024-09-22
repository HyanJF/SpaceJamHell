using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDMG : MonoBehaviour
{
    public float bulletDMG;

    [SerializeField] float bulletLife;
    float bulletTime = 0;

    void Update()
    {
        bulletTime += Time.deltaTime;
        if (bulletTime > bulletLife)
        {
            Destroy(gameObject);
            bulletTime = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
            other.GetComponent<PlayerStats>().Damage(bulletDMG);
            Destroy(gameObject);
        }
    }
}
