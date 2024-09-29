using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPlayerZone : MonoBehaviour
{
    public GameObject larry;  
    public GameObject spawnPoint; 
    public bool isFalling = false;   

    public static SpawnPlayerZone spawnPlayerZone; 

    private void Awake()
    {
        if(spawnPlayerZone == null) spawnPlayerZone = this;
        else Destroy(this);
    }
    private void Start()
    {
        SpawnPlayer();
    }

    private void Update()
    {
        if(isFalling)
        {
            SpawnPlayer();
            isFalling = false;
        }
    }
    public void SpawnPlayer()
    {
        if (larry != null && spawnPoint != null)
        {
            larry.transform.position = spawnPoint.transform.position;
        }
    }
}
