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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit");
            PlayerStats.instance.Damage(bulletDMG);
            PlayerV2Manager.playerV2Manager.ApplyKnockback(3);
            Destroy(gameObject);
        }
    }
}
