using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    //public static event Action OnPlayerDeath;
    public static Action OnPlayerDamage;
    public int curHealth;
    public int maxHealth = 3;

    public float damageCool = 1.5f;
    public float lastDamaged;


    public GameObject gameOverMenu;


    // Start is called before the first frame update
    // start the hp and make sure game over cannot be seen
    void Start()
    {
        lastDamaged = Time.time;
        curHealth = maxHealth;  
        gameOverMenu.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        //OnPlayerDeath?.Invoke()
            
            curHealth -= damage;
            OnPlayerDamage?.Invoke();
            if (curHealth <= 0)
            {
                curHealth = 0;
                gameOverMenu.SetActive(true);
                Time.timeScale = 0;
            }
        
      
    }
    public void damTime()
    {
        lastDamaged = Time.time;
    }

    public void goToMenu()
    {
   
    }
    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
