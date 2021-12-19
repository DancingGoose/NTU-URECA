using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject player;

    // private void OnTriggerEnter(Collider other) 
    // {
    //     if(other.gameObject == player)
    //     {
    //         player.transform.parent = transform;
    //     }
    // }

    // private void OnTriggerExit(Collider other) 
    // {
    //     if(other.gameObject == player)
    //     {
    //         player.transform.parent = null;
    //     }
    // }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}
