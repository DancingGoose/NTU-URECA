using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDetector : MonoBehaviour
{
    public bool detectSomething;

    // Start is called before the first frame update
    void Start()
    {
        detectSomething = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" || other.tag == "Bee")
        {
            detectSomething = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obstacle" || other.tag == "Bee")
        {
            detectSomething = false;
        }
    }
}
