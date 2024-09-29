using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroidObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(other.gameObject);
        }
    }
}
