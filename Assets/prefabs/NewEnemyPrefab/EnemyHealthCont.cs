using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealthCont : MonoBehaviour
{
    public int maxHealth = 500;
    public int currentHealth;

    public string sceneName;

    public HealthBar healthBar;
    public GameManagerScript gameManager;

    public AudioClip audioClip;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        //TakeDamage(20);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the collision is with a circle collider
            Collider2D circleCollider = collision.collider as CircleCollider2D;
            if (circleCollider != null)
            {
                TakeDamage(30);
                PlayAudioClip();
            }
            PlayAudioClip();
        }

        if (currentHealth <= 0)
        {
            UnlockNewLevel();
            Die();
        }

    }
   

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
    void Die()
    {
        SceneManager.LoadScene(sceneName);
        // Player's health is reduced to zero
        //gameManager.Dialogue();
        gameManager.gameWin();
        //Time.timeScale = 0; // Pause the game
    }
    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    private void PlayAudioClip()
    {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
        }
    }
}
