using UnityEngine;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            UIMethod.uIMethod.WinMenu();
        }
        
    }
}
