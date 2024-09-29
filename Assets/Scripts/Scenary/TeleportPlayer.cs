using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpawnPlayerZone.spawnPlayerZone.isFalling = true;
            PlayerStats.instance.Damage(10);
            Debug.Log("Puto el que no reaccione");
        }
    }
}

