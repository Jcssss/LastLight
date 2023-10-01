using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HorizontalStartDirection {Right, Left}
public class HorizontalMovingPlatform : MonoBehaviour
{
    public float moveSpeed;	
    [SerializeField] private float travelDistanceRight;	
    [SerializeField] private float travelDistanceLeft;	
    [SerializeField] private HorizontalStartDirection initialDirecion;
    private Vector3 initialPosition;

    void Awake()
    {
        if (initialDirecion == HorizontalStartDirection.Left) {
            moveSpeed *= -1;
        }

        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);

        if (moveSpeed < 0 && transform.position.x <= initialPosition.x - travelDistanceLeft) {
            moveSpeed *= -1;
        } else if (moveSpeed > 0 && transform.position.x >= initialPosition.x + travelDistanceRight) {
            moveSpeed *= -1;
        }
    }
}
