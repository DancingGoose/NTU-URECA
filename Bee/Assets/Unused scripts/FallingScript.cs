using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingScript : MonoBehaviour
{
    public GameObject player;

    // public int x = 0;
    // public int y = 6;
    // public int z = 0;

    public static Vector3 checkPoint = new Vector3(0, 6, 0);

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < -20)
        {
            player.transform.position = checkPoint;
        }
    }
}
