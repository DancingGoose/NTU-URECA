using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLighting : MonoBehaviour
{
    // Start is called before the first frame update

    public Color[] colors;
    public Light lightObj;

    // Update is called once per frame
    void Update()
    {
        int n = (int)(Time.time) % 3;
        Color startColor = colors[n];
        if (n > 1)
        {
            n = 0;
        }
        else
        {
            n++;
        }
        Color endColor = colors[n];
        float scaledTime = Time.time - (float) Math.Floor((Time.time));
        lightObj.color = Color.Lerp(startColor, endColor, scaledTime);
    }
}
