using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float health, maxHealth = 100, lerpSpeed;
    public Image healthBar;
    public static PlayerStats instance;

    public List<AudioClip> audioClips;
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        if (health > maxHealth) health = maxHealth;

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        ColorChanger();
    }

    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);
    }

    public void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
    }

    public void Damage(float damagePoints)
    {
        if(health > 0)
        {
            health -= damagePoints;
            int r = Random.Range(0, audioClips.Count);
            AudioClip clip = audioClips[r];
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void Heal(float healingPoints)
    {
        if(health < maxHealth)
            health += healingPoints;
    }

}
