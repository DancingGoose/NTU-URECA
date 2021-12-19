using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HiveScript : MonoBehaviour
{
    /* VARIABLES:
     * 
     * 
     * 
     */
    public GameObject playerObj, textObj, points;
    Collider playerCol;
    public static Vector3 position;
    public static IDictionary<string, Vector3> hivePoints;
    public static bool playerInside, allDone;
    public static int nectarDelivered, nectarNotDelivered;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("BeePlayer");
        playerCol = playerObj.GetComponent<Collider>();
        playerInside = false;
        nectarDelivered = 0;
        nectarNotDelivered = 0;
        hivePoints = new Dictionary<string, Vector3>();
        position = transform.position;
        foreach (Transform child in points.transform)
        {
            hivePoints.Add(child.name, child.position);
        }
        allDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerInside && PlayerControlScript.bringNectar)
        {
            PlayerControlScript.bringNectar = false;
            nectarDelivered++;
            PlayerControlScript.nectarDelivered++;
        }
        textObj.GetComponent<TextMeshPro>().text = nectarDelivered.ToString();
        if(FlowersScript.count == (nectarNotDelivered + nectarDelivered))
        {
            allDone = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == playerCol)
            playerInside = true;
        else if (other.tag == "Bee")
            other.GetComponent<BeeScript>().inHive = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other == playerCol)
            playerInside = false;
        else if (other.tag == "Bee")
            other.GetComponent<BeeScript>().inHive = false;
    }

    Vector3 addVector(Vector3 v, float x, float y)
    {
        Vector3 newVector = v;
        newVector.x += x;
        newVector.y += y;
        return newVector;
    }
}
