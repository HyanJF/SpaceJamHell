using UnityEngine;

public class DamageBoss : MonoBehaviour
{
    public float playerDMG;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Boss detected Player");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Boss Hit");
            SpawnPlayerZone.spawnPlayerZone.isFalling = true;
            Life.instance.Damage(playerDMG);
        }

        if (Life.instance.health <= 0)
        {
            Debug.Log("Boss killed");
            Life.instance.health = 0;
            Life.instance.healthBar.fillAmount = 0;
            Destroy(gameObject);
        }
    }
}
