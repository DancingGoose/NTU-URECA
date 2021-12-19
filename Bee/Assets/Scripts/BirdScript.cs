using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public float birdSpeed = 5, sightRadius = 50, startWaitTime = 3f;
    private float xStart, zStart, yConstant, waitTime;
    GameObject chasedBee;
    Transform chasedBeeTrans;
    Vector3 endPosition;
    bool rest;

    /* FUNCTIONS:
     * 
     * Start is called before the first frame update
     * Update is called once per frame
     */

    // Start is called before the first frame update
    void Start()
    {
        chasedBee = null;
        chasedBeeTrans = null;
        rest = false;
        xStart = transform.position.x;
        zStart = transform.position.z;
        yConstant = transform.position.y;
        birdSpeed = Random.Range(5f, 10f);
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        yConstant = transform.position.y;
        if (chasedBee != null)
        {
            FaceTarget(chasedBeeTrans.position);
            transform.Translate(Vector3.forward * Time.deltaTime * birdSpeed, Space.Self);
            if(Vector3.Distance(chasedBeeTrans.position, transform.position) <= 4f)
            {
                if (chasedBee.name == "BeePlayer")
                {
                    AllUIController.isDead = true;
                }
                FlowersScript.occupied[chasedBee.GetComponent<BeeScript>().point] = false;
                if(chasedBee.GetComponent<BeeScript>().bringNectar)
                    HiveScript.nectarNotDelivered += 1;
                
                Destroy(chasedBee);
                chasedBee = null;
                chasedBeeTrans = null;
            }
        }
        else if(rest)
        {
            Invoke("WakeFromRest", 1f);
        }
        else
        {
            Patrol();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bee" && chasedBee == null && !other.GetComponent<BeeScript>().escape)
        {
            chasedBee = other.gameObject;
            chasedBeeTrans = chasedBee.GetComponent<Transform>();
            birdSpeed = Random.Range(35f, 45f);
        }
        else if (other.tag == "Flower")
        {
            FlowersScript.dangerous[other.GetComponent<FlowerScript>().ID] = true;
            //ToggleDangerFlower(other.GetComponent<FlowerScript>().ID, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == chasedBee)
        {
            chasedBee = null;
            chasedBeeTrans = null;
            rest = true;
            birdSpeed = Random.Range(5f, 10f);
        }
        else if (other.tag == "Flower")
        {
            FlowersScript.dangerous[other.GetComponent<FlowerScript>().ID] = false;
            //ToggleDangerFlower(other.GetComponent<FlowerScript>().ID, false);
        }
    }

    void FaceTarget(Vector3 V)
    {
        Vector3 direction = (V - transform.position).normalized;
        if (direction != new Vector3(0f, 0f, 0f))
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, endPosition) < 0.3f)
        {
            if (waitTime <= 0)
            {
                transform.LookAt(endPosition);
                Reset();
            }
            else
                waitTime -= Time.deltaTime;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            FaceTarget(endPosition);
            transform.position = Vector3.MoveTowards(transform.position, endPosition, birdSpeed * Time.deltaTime);
            yConstant = transform.position.y;
        }
    }

    void WakeFromRest()
    {
        rest = false;
    }

    void Reset()
    {
        float dist = 0, xEnd = 0, zEnd = 0;
        Vector3 tempPosition;
        while (dist < sightRadius/4)
        {
            xEnd = xStart + Random.Range(sightRadius / 2, sightRadius);
            zEnd = zStart + Random.Range(sightRadius / 2, sightRadius);
            tempPosition = new Vector3(xEnd, yConstant, zEnd);
            dist = Vector3.Distance(tempPosition, transform.position);
        }
        endPosition = new Vector3(xEnd, yConstant, zEnd);
        waitTime = startWaitTime;
    }

    void ToggleDangerFlower(int ID, bool X)
    {
        FlowersScript.dangerous[ID] = X;
    }
}
