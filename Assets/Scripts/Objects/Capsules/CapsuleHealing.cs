using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleHealing : MonoBehaviour
{
    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerStats.instance.Heal(25);
            audioSource.Play();
            Destroy(gameObject);
        }
        
    }
}
