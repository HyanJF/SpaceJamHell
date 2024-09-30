using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public float health, maxHealth = 3, lerpSpeed;
    public Image healthBar;
    public static Life instance;

    public List<AudioClip> audioClips;
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth)
            health = maxHealth;

        lerpSpeed = 3f* Time.deltaTime;

        HealthBarFiller();
        ColorChange();
    }

    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);
    }

    public void ColorChange()
    {
        Color healthColor = Color.Lerp(Color.black, Color.red, (health / maxHealth));
        healthBar.color = healthColor;
    }

    public void Damage(float damagePoints)
    {
        if (health > 0)
        {
            int r = Random.Range(0, audioClips.Count);
            AudioClip clip = audioClips[r];
            audioSource.clip = clip;
            audioSource.Play();
            health -= damagePoints;
        }
    }
}
