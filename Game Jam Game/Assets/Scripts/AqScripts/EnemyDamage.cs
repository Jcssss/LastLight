using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;
    public playerHealth playerhp;
    public bool canDamage = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(playerhp.lastDamaged);
        Debug.Log(playerhp.lastDamaged + playerhp.damageCool);
        Debug.Log(Time.time);
        if (playerhp.lastDamaged + playerhp.damageCool < Time.time)
        {
            canDamage = true;
        }
        if (collision.gameObject.tag == "Player" && canDamage )
        {   
            playerhp.GetComponent<playerHealth>().TakeDamage(damage);
            playerhp.damTime();
            canDamage = false;

        }
       
        if (collision.gameObject.tag == "Pixie")
        {
            //playerhp.TakeDamage(damage);
        }
    }

}
