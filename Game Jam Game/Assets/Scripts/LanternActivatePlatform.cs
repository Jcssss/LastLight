using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LanternActivatePlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;
    [SerializeField] private bool beginOn;

    public void Awake () {
        GetComponent<Light2D>().intensity = (beginOn)? 1 : 0;
    }

    public void Activate () {
        GetComponent<Light2D>().intensity = 1;
        foreach (GameObject platform in platforms) {
            platform.GetComponent<MovingPlatform>().Activate();
        }
    }
}
