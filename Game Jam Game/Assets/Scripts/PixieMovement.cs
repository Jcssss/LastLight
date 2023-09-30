using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixieMovement : MonoBehaviour
{
    public float speed = 3.0f;

    private Vector3 target;

    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3f;

    private bool mouseDown = false;
    private bool canFire = true;
    private bool firing = false;

    private float lastFired;
    public float firingCooldown = 3.0f;

    public float detectionRadius = 1.0f;
    public ContactFilter2D ContactFilter;
	

    void Update() {
        // holding down mouse button
        if(Input.GetMouseButton(0)) {
            mouseDown = true;
        } else {
            mouseDown = false;
        }

        // get surroundings
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, detectionRadius, ContactFilter.NoFilter(), results);

        foreach (Collider2D target in results) {

            if(target.gameObject.tag.Equals("Lantern")) {
                target.gameObject.GetComponent<Lantern>().Activate();
            }
        }
            

    }

    // Update is called once per frame
    void FixedUpdate() {	
        
        if(mouseDown && canFire){
            firing = true;
            canFire = false;
            lastFired = Time.time;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
        }

        if (firing) {
            Vector3 newPos = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);

            // if arrived at target
            Vector3 offset = transform.position - newPos;
            float sqrLen = offset.sqrMagnitude;

            if(sqrLen < 0.00001f) {
                //firing = false;
            } else {
                transform.position = newPos;
            }
            
        }

        if (lastFired + firingCooldown < Time.time) {
            canFire = true;
        }
    }
}
