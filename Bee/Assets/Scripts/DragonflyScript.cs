using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyScript : MonoBehaviour
{
    public float dragonflySpeed = 20, sightRadius = 50, startWaitTime = 3f;
    private float waitTime = 3;
    public int point, total;
    public static List<bool> occupied = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        point = 0;
        total = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (total == 0)
            initialize();
        Patrol(Normalizer(FlowersScript.positions[point]));
    }

    void initialize()
    {
        total = FlowersScript.positions.Count;
        while (occupied.Count < total)
        {
            occupied.Add(false);
        }
        while (occupied[point])
        {
            point = Random.Range(0, total);
        }
        occupied[point] = true;
    }

    Vector3 Normalizer(Vector3 V)
    {
        Vector3 newV = V;
        newV.y += 4.5f;
        newV.x += 1f;
        return newV;
    }

    void FaceTarget(Vector3 V)
    {
        Vector3 direction = (V - transform.position).normalized;
        if (direction != new Vector3(0f, 0f, 0f))
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            // Debug.Log(lookRotation);
        }

    }

    void Patrol(Vector3 V)
    {
        if (Vector3.Distance(transform.position, V) < 0.0001f)
        {
            if (waitTime <= 0)
            {
                transform.LookAt(V);
                Reset();
            }
            else
                waitTime -= Time.deltaTime;
            //Debug.Log(waitTime.ToString());
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            FaceTarget(V);
            transform.position = Vector3.MoveTowards(transform.position, V, dragonflySpeed * Time.deltaTime);
        }
    }

    void Reset()
    {
        waitTime = startWaitTime;
        occupied[point] = false;
        int temp = point;

        while (point == temp || occupied[point])
        {
            point = Random.Range(0, total);
        }

        occupied[point] = true;
    }
}
