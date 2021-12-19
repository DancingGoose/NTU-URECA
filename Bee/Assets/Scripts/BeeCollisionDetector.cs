using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCollisionDetector : MonoBehaviour
{
    public bool detectSomething, up, down, left, right;
    public GameObject UP, DOWN, LEFT, RIGHT;
    public Vector3 upPosition, downPosition, leftPosition, rightPosition;
    AudioSource scoreSound;

    // Start is called before the first frame update
    void Start()
    {
        up = down = left = right = detectSomething = false;
        scoreSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        up = UP.GetComponent<MiniDetector>().detectSomething;
        down = DOWN.GetComponent<MiniDetector>().detectSomething;
        left = LEFT.GetComponent<MiniDetector>().detectSomething;
        right = RIGHT.GetComponent<MiniDetector>().detectSomething;
        upPosition = UP.transform.position;
        downPosition = DOWN.transform.position;
        leftPosition = LEFT.transform.position;
        rightPosition = RIGHT.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle" || other.tag == "Bee" || other.tag == "Player")
        {
            detectSomething = true;
        }

        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obstacle" || other.tag == "Bee" || other.tag == "Player")
        {
            detectSomething = false;
        }
    }
    
}
