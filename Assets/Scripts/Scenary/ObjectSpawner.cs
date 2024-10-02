using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] objectsToSpawn;
    public float spawnInterval = 0.9f;

    public static ObjectSpawner os;
    private void Awake()
    {
        if (os == null) os = this;
        else Destroy(this);
    }
    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnObjectAtRandomPoint();
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    public void SpawnObjectAtRandomPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No hay puntos de spawn asignados.");
            return;
        }

        if (objectsToSpawn.Length == 0)
        {
            Debug.LogWarning("No hay objetos para spawnear.");
            return;
        }

        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform selectedSpawnPoint = spawnPoints[randomSpawnIndex];

        int randomObjectIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject objectToSpawn = objectsToSpawn[randomObjectIndex];

        GameObject spawnedObject = Instantiate(objectToSpawn, selectedSpawnPoint.position, selectedSpawnPoint.rotation);
        Debug.Log("Objeto generado: " + spawnedObject.name + " en la posición: " + spawnedObject.transform.position);
    }
}
