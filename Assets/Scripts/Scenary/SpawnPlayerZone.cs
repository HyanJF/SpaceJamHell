using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPlayerZone : MonoBehaviour
{
    public GameObject larry;  
    public Vector3 spawnPoint; 
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
            PlayerV2Manager.playerV2Manager.controller.enabled = false;
            SpawnPlayer();
            isFalling = false;
        }
    }
    public void SpawnPlayer()
    {
        if (larry != null && spawnPoint != null)
        {
            larry.transform.position = spawnPoint;
            PlayerV2Manager.playerV2Manager.controller.enabled = true;
        }
    }
}
