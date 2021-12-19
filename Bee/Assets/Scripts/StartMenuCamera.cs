using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 10f*Time.deltaTime, 0);
    }
}
