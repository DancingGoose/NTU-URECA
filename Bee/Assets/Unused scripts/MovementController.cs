using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("sphereAnimation", 2.0f);
    }

    void sphereAnimation()
    {
        GameObject.Find("Sphere1").GetComponent<Animator>().SetBool("isMoving", true);
        
        GameObject.Find("Sphere2").GetComponent<Animator>().SetBool("isMoving", true);
        
        Invoke("cubeAnimation", 2.0f);
    }

    void cubeAnimation()
    {
        GameObject.Find("MovingCube").GetComponent<Animator>().SetBool("isMoving", true);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
