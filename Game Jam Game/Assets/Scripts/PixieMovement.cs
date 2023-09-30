using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixieMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float detectionRadius = 1.0f;
    public Transform offsetToPlayer;
    public float firingCooldown = 3.0f;

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3f;

    // firing variables
    private bool mouseDown = false;
    private bool mouse2Down = false;
    private bool canFire = true;
    private bool firing = false;
    private float lastFired;


    // filter for collisions
    public ContactFilter2D ContactFilter;

    // attachment variables
    private bool isAttached = false;
    
    // idle variables
    public float idleBobFrequency = 1.0f;
    public float idleBobMagnitude = 1.0f;
	

    void Update() {
        if(Input.GetMouseButton(0)) {
            mouseDown = true;
        } else {
            mouseDown = false;
        }

        if(Input.GetMouseButton(1)) {
            mouse2Down = true;
        } else {
            mouse2Down = false;
        }

        // get surroundings
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, detectionRadius, ContactFilter.NoFilter(), results);

        foreach (Collider2D hit in results) {

            if(hit.gameObject.tag.Equals("Lantern")) {
                hit.gameObject.GetComponent<Lantern>().Activate();
            } else if (hit.gameObject.tag.Equals("Player") && !firing) {
                Attach();
            }
        }
            

    }

    void FixedUpdate() {	
        
        if(mouseDown && canFire && isAttached){
            Fire();
            Detach();
        }

        if (firing) {
            // check if done firing
            Vector3 difference = transform.position - targetPosition;
            float sqrDiff = Vector3.SqrMagnitude(difference);
            if(sqrDiff < 0.001f) firing = false;

            MoveToward(targetPosition);

        } else if(mouse2Down) {
            Attach();
        }

        if (lastFired + firingCooldown < Time.time) {
            canFire = true;
        }

        // idle bob
        if(idleBobFrequency <= 0) idleBobFrequency = 0.001f;
        Vector3 pos = isAttached? offsetToPlayer.position : targetPosition;
        pos.y += Mathf.Sin(Time.time / idleBobFrequency) * idleBobMagnitude;
        MoveToward(pos);
    }

    private void Fire() {
        firing = true;
        canFire = false;
        lastFired = Time.time;
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;
    }

    private void Attach() {
        isAttached = true;
    }

    private void Detach() {
        isAttached = false;
    }

    private void MoveToward(Vector3 targetPosition) {
        Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        newPos.z = 0;
        transform.position = newPos;
    }

    void OnValidate() {
        if(idleBobFrequency < 0) {
            idleBobFrequency = 0;
        }
    }
}
