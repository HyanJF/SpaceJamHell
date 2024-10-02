using UnityEngine;

public class Songs : MonoBehaviour
{
    public AudioClip song1, song2, song3, song4;
    public AudioSource audioSource;
    public GameObject win;
    private float currentHealth;

    private void Start()
    {
        currentHealth = Life.instance.health;
        audioSource.clip = song1;
        audioSource.Play();
    }

    private void Update()
    {
        float health = Life.instance.health; 

        if (health != currentHealth) 
        {
            currentHealth = health; 
            PlaySongBasedOnHealth(currentHealth); 
        }
    }

    private void PlaySongBasedOnHealth(float health)
    {
        switch (health)
        {
            case 2:
                audioSource.clip = song2;
                ObjectSpawner.os.spawnInterval = 0.5f;
                break;
            case 1:
                audioSource.clip = song3;
                ObjectSpawner.os.spawnInterval = 0.35f;
                break;
            case 0:
                audioSource.clip = song4;
                ObjectSpawner.os.spawnInterval = 0.15f;
                win.SetActive(true);
                break;
            default:
                audioSource.clip = song1;
                break;
        }
        
        audioSource.Play(); 
    }
}
