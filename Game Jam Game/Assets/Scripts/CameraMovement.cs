using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform target;
    public CharacterController2D controller;

    private float cameraHeight = 0;
    private float cameraDepth = -10f;

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    public float leadingCameraOffset = 0f;


    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 targetPosition = target.position + offset;
        targetPosition.y = cameraHeight;
        targetPosition.x += controller.FacingRight() ? leadingCameraOffset : -leadingCameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    }

    // for sticking directly on target
    Vector3 TargetPosition() {
        Vector3 pos = target.position;
        pos.z = cameraDepth;

        return pos;
    }

    // for only following target x
    Vector3 TargetX() {
        return new Vector3(target.position.x, cameraHeight, cameraDepth);
    }
}
