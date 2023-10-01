using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public Transform target2;
    public CharacterController2D controller;

    [Range(0,1)]
    public float targetWeighting;

    private float cameraDepth = -10;

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    public float leadingCameraOffset = 0f;


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition = targetWeighting * target.position + (1 - targetWeighting) * target2.position;
        targetPosition.z = cameraDepth;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    }
}
