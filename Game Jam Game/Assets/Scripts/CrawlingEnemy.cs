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

    // jump stuff
    public float jumpForce;
    public float jumpHeight;
    private bool canJump = true;
    private bool jumping = false;
    private float jumpDistance;

    // mode
    bool isAggro = false;

    // rigidbody movement
    Vector3 v = Vector3.zero; 
    float smoothTime = 0.3f;
    

    void Start()
    {
        pixiePos = pixie.GetComponent<Transform>();
        initialPos = GetComponent<Transform>().position;

        trans = this.transform;
        rb = this.GetComponent<Rigidbody2D>();
        width = GetComponent<SpriteRenderer>().bounds.extents.x;

        jumpDistance = distance * 0.75f;
    }

    void FixedUpdate() {


        //go towards player if in range
        if (playerIsWithin(distance) || isAggro) {
            /*
            Vector3 temp = Vector2.MoveTowards(transform.position, pixiePos.position, speed * Time.deltaTime);
            temp.y = transform.position.y;
            transform.position = temp;
            */
        
        
            if(!isGrounded()) return;


            // Move the character by finding the target velocity
            Vector3 dir = pixiePos.position - transform.position;
            dir = Vector3.Normalize(dir);
			Vector3 targetVelocity = new Vector2(dir.x * 10f, rb.velocity.y);
			// And then smoothing it out and applying it to the character
			rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref v, smoothTime);

            // if in closer distance, jump
            // can't jump again until pixie reenters said closer distance
            if (playerIsWithin(jumpDistance)) {
                if(canJump && isGrounded()) {
                    jumpAtTarget(pixiePos.position);
                    canJump = false;
                }
            } else {
                canJump = true;
            }

            // once see target, become aggro
            isAggro = true;
        } else {
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

    void jumpAtTarget(Vector3 targetPos) {
        Vector3 force = (targetPos - transform.position) * jumpForce;
        force.y += jumpHeight; // aim for the head
        rb.AddForce(force);
        canJump = false;
        jumping = true;
    }

    bool isGrounded() {
        Vector2 lineCastPos = trans.position + (trans.right * width);
        return Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
    }

    bool playerIsWithin(float d) {
        return Vector2.Distance(transform.position, pixiePos.position) < d;
    }
}
