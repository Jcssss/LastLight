using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PaceOnPlatform : MonoBehaviour
{
    public LayerMask enemyMask;
    public float speed;
    Rigidbody2D rb;
    Transform trans;
    float width;

    void Start()
    {
        trans = this.transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        //get linecast for bottom corner of enemy in direction its moving
        Vector2 lineCastPos = trans.position + (trans.right * width) * (rb.velocity.x > 0 ? 1 : -1);

        //returns true if line is colliding with anything other than stuff on the enemy layer (NEED TO ADD ENEMY LAYER)
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

        //if no ground, turn around
        if (!isGrounded)
        {
            Vector3 rotation = trans.eulerAngles;
            rotation.y += 180;
            trans.eulerAngles = rotation;
        }
        //always move forward
        Vector2 vel = rb.velocity;
        vel.x = trans.right.x * speed;
        rb.velocity = vel;

    }
}
