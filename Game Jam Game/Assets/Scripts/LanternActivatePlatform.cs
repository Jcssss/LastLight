using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternActivatePlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;

    public void Activate () {
        foreach (GameObject platform in platforms) {
            platform.GetComponent<MovingPlatform>().Activate();
        }
    }
}
