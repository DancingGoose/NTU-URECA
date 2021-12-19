using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject lightObj;
    public static Light lightComp;
    public static List<Color> colors = new List<Color>();

    /* FUNCTIONS:
     * 
     * Start is called before the first frame update
     * Update is called once per frame
     */

    // Start is called before the first frame update
    void Start()
    {
        colors.Add(new Color(255, 244, 214, 255)/255);
        colors.Add(new Color(244, 214, 255, 255)/255);
        lightComp = lightObj.GetComponent<Light>();
    }

    public static void changeLight(int i)
    {
        lightComp.color = colors[i];
    }
}
