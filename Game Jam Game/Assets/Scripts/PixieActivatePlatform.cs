using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixieActivatePlatform : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "MovingPlatformLantern")
        {
            other.gameObject.GetComponent<LanternActivatePlatform>().Activate();
        }
    }
}
