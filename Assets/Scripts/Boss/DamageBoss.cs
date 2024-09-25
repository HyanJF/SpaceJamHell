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
            Life.instance.Damage(playerDMG);
            PlayerV2Manager.playerV2Manager.ApplyKnockback(150);
        }

        if (Life.instance.health <= 0)
        {
            Debug.Log("Boss killed");
            Life.instance.health = 0;
            Destroy(gameObject);
        }
    }
}
