using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StartDirection {UpRight, DownLeft}
public enum Movement {Horizontal, Vertical}
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveSpeed;	
    [SerializeField] private float travelDistanceUpRight;	
    [SerializeField] private float travelDistanceDownLeft;	
    [SerializeField] private StartDirection initialDirecion;
    [SerializeField] private Movement movement;
    [SerializeField] private bool isActivatable;
    public GameObject light;
    private Vector3 initialPosition;
    private float curMoveSpeed;

    void Awake()
    {
        if (initialDirecion == StartDirection.DownLeft) {
            moveSpeed *= -1;
        }

        initialPosition = transform.position;

        curMoveSpeed = (isActivatable)? 0 : moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {   
        if (movement == Movement.Vertical)
        {
            transform.position += new Vector3(0, curMoveSpeed * Time.deltaTime, 0);

            if (curMoveSpeed < 0 && transform.position.y <= initialPosition.y - travelDistanceDownLeft) {
                curMoveSpeed *= -1;
            } else if (curMoveSpeed > 0 && transform.position.y >= initialPosition.y + travelDistanceUpRight) {
                curMoveSpeed *= -1;
            }
        } else {
            transform.position += new Vector3(curMoveSpeed * Time.deltaTime, 0, 0);

            if (curMoveSpeed < 0 && transform.position.x <= initialPosition.x - travelDistanceDownLeft) {
                curMoveSpeed *= -1;
            } else if (curMoveSpeed > 0 && transform.position.x >= initialPosition.x + travelDistanceUpRight) {
                curMoveSpeed *= -1;
            }
        }
    }

    public void Activate () {
        Debug.Log("hello");
        if (isActivatable && curMoveSpeed == 0)
        {
            curMoveSpeed = moveSpeed;
            light.SetActive(true);
        }
    }
}
