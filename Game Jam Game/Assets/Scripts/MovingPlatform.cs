using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum StartDirection {UpRight, DownLeft}
public enum Movement {Horizontal, Vertical}
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveSpeed;	
    [SerializeField] private float travelDistanceUpRight;	
    [SerializeField] private float travelDistanceDownLeft;	
    [SerializeField] private float moveTime;
    [SerializeField] private StartDirection initialDirecion;
    [SerializeField] private Movement movement;
    [SerializeField] private bool isActivatable;
    [SerializeField] private float lightFlickerFrequency;
    [Range(0, 1)][SerializeField] private float minLightIntensity;
    public GameObject light;
    private Vector3 initialPosition;
    private float curMoveSpeed;
    private float timeSinceStart;
    private float intensityIncrements;

    void Awake()
    {
        if (initialDirecion == StartDirection.DownLeft) {
            moveSpeed *= -1;
        }

        initialPosition = transform.position;

        curMoveSpeed = (isActivatable)? 0 : moveSpeed;

        intensityIncrements = -(1 - minLightIntensity) / lightFlickerFrequency;
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

        if (curMoveSpeed != 0) {
            timeSinceStart += Time.deltaTime;
            if (timeSinceStart >= moveTime) {
                curMoveSpeed = 0;
                light.SetActive(false);
            }

            Debug.Log(light.GetComponent<Light2D>().intensity);
            light.GetComponent<Light2D>().intensity += intensityIncrements * Time.deltaTime;
            if (intensityIncrements >= 0 && light.GetComponent<Light2D>().intensity >= 1) {
                intensityIncrements *= -1;
            } else if (intensityIncrements <= 0 && light.GetComponent<Light2D>().intensity <= minLightIntensity) {
                intensityIncrements *= -1;
            }
        }
    }

    public void Activate () {
        if (isActivatable && curMoveSpeed == 0)
        {
            curMoveSpeed = moveSpeed;
            light.SetActive(true);
            timeSinceStart = 0;
            light.GetComponent<Light2D>().intensity = 1;
            intensityIncrements = -Mathf.Abs(intensityIncrements);
        }
    }
}
