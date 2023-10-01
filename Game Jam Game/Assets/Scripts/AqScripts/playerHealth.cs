using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public static Action OnPlayerDamage;
    public int curHealth;
    public int maxHealth = 3;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;  
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        OnPlayerDamage?.Invoke();
        if (curHealth <= 0)
        {
            //death?
            //might need/want animation?
            //Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
