using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerV2Manager>().ApplyKnockback(250);
        }
    }
}
