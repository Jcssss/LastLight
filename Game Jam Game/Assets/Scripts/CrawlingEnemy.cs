using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlingEnemy : MonoBehaviour
{
    //stop at edge stuff
    public LayerMask enemyMask;
    public float speed;
    Rigidbody2D rb;
    Transform trans;
    float width;
    //Chase stuff
    public GameObject player;
    private Transform playerPos;
    private Vector2 currentPos;
    public float distance;

    void Start()
    {
        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;

        trans = this.transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //get linecast for bottom corner of enemy in direction its moving
        Vector2 lineCastPos = trans.position + (trans.right * width) * (rb.velocity.x > 0 ? 1 : -1);

        //returns true if line is colliding with anything other than stuff on the enemy layer
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
