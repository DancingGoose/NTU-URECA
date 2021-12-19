using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMode : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject playerObj;
    Vector3 cameraOffSet;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    
    void Start()
    {
        playerObj = GameObject.Find("BeePlayer");
        cameraOffSet = new Vector3(0,1,-5);
    }

    // Update is called once per frame
    void Update()
    {
        
        // 3rd Person
        // transform.position = playerObj.transform.position + cameraOffSet;
        // 1st Person
        // playerObj.GetComponent<Renderer>().bounds.size.y / 4
        //Vector3 toHead = new Vector3(0, 0, 0);
        // transform.position = playerObj.transform.position + toHead;
        transform.position = playerObj.transform.position;
        transform.eulerAngles = playerObj.transform.eulerAngles;
    }

    void ChangeBody(string newBody)
    {
        playerObj = GameObject.Find(newBody);
    }
}
