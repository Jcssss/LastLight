using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
      //  playerHealth.OnPlayerDeath += EnableGameOver;
    }

    private void OnDisable()
    {
        //playerHealth.OnPlayerDeath -= EnableGameOver;
    }
    public void EnableGameOver()
    {
        gameOverMenu.SetActive(true);
    }
}
