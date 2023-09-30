using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    //contact filter for collisions
    public ContactFilter2D ContactFilter;

    public Light2D lantern;
    public float lightSmoothing = 0.6f;
    public lightLevel[] lightLevels;

    public bool isActive = false;

    // SmoothDamp Variables
    private float intensityV;
    private float innerV;
    private float outerV;

    // Update is called once per frame
    void Update()
    {
        //changeLight(lightLevels[curLightLevel]);

        int index = isActive ? 1 : 0;
        lightLevel l = lightLevels[index];
        changeLight(l);

        //enemy killing
        if (isActive)
        {
            List<Collider2D> results = new List<Collider2D>();
            Physics2D.OverlapCircle(transform.position, l.outerRadius, ContactFilter.NoFilter(), results);

            foreach (Collider2D hit in results)
            {
                if (hit.gameObject.tag.Equals("Enemy"))
                {
                    Destroy(hit.gameObject);
                }
            }
        }
    }

    public void Activate() {
        isActive = true;
    }

    void changeLight(lightLevel l) {
        lantern.intensity = Mathf.SmoothDamp(lantern.intensity, l.intensity, ref intensityV, lightSmoothing);
        lantern.pointLightInnerRadius = Mathf.SmoothDamp(lantern.pointLightInnerRadius, l.innerRadius, ref innerV, lightSmoothing);
        lantern.pointLightOuterRadius = Mathf.SmoothDamp(lantern.pointLightOuterRadius, l.outerRadius, ref outerV, lightSmoothing);
    }

    [System.Serializable]
    public struct lightLevel {
        public float intensity;
        public float innerRadius;
        public float outerRadius;
    }

    private void OnValidate() {
        //curLightLevel = Mathf.Clamp(curLightLevel, 0, lightLevels.Length - 1);
    }
}


