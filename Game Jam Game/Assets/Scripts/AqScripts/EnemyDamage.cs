using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;
    public playerHealth playerhp;

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Player") {
            playerhp.GetComponent<playerHealth>().TakeDamage(damage);
        }
       if (collision.gameObject.tag == "Pixie")
        {
            playerhp.TakeDamage(damage);
        }
    }

}
