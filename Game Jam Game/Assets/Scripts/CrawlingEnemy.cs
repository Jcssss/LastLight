using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlingEnemy : MonoBehaviour
{
    //stop at edge stuff
    public LayerMask enemyMask;
    public float speed;
    Transform trans;
    Rigidbody2D rb;
    float width;

    //Chase stuff
    public GameObject pixie;
    private Transform pixiePos;
    private Vector2 initialPos;
    public float distance;

    void Start()
    {
        pixiePos = pixie.GetComponent<Transform>();
        initialPos = GetComponent<Transform>().position;

        trans = this.transform;
        rb = this.GetComponent<Rigidbody2D>();
        width = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    private void FixedUpdate()
    {
        //go towards player if in range
        if (Vector2.Distance(transform.position, pixiePos.position) < distance)
        {
            Vector3 rotation = trans.eulerAngles;
            //the ? statement doesn't seem to work to make it turn around and idk why
            transform.position = Vector2.MoveTowards(transform.position, pixiePos.position, speed * Time.deltaTime * (rotation.y == 0 ? -1 : 1));
        }
        else
        {
            if (Vector2.Distance(transform.position, initialPos) <= 0)
            {

            }
            else //otherwise go back and forth on platform
            {
                //get linecast for bottom corner of enemy in direction its moving
                Vector2 lineCastPos = trans.position + (trans.right * width);

                //returns true if line is colliding with anything other than stuff on the enemy layer
                bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

                //if no ground, turn around
                if (!isGrounded)
                {
                    Vector3 rotation = trans.eulerAngles;
                    rotation.y = (rotation.y == 0 ? 180 : 0);
                    trans.eulerAngles = rotation;
                }
                //always move forward
                Vector2 vel = rb.velocity;
                vel.x = trans.right.x * speed;
                rb.velocity = vel;
            }
        }

        
    }
}
