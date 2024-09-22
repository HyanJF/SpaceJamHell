using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleHealing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerStats>().Heal(15);
            Destroy(gameObject);
        }
        
    }
}
