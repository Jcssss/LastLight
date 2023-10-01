using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float minIntensity;

    private float curTime = 0;	

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
    }
}
