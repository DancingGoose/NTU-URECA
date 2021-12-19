using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragonflyMove : MonoBehaviour
{
    
    Rigidbody dragonRB;
    float v = 5;
    float rotateV = 10;
    bool turnLeft = false;
    bool turnRight = false;
    Quaternion from;
    Quaternion to;
    private float timeCount = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //dragonRB = gameObject.transform.GetChild(0).GetComponent<Rigidbody>();
        from = gameObject.transform.rotation;
        to = gameObject.transform.rotation;
        //dragonRB.velocity = new Vector3(v, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //dragonRB.velocity = new Vector3(v, 0, 0);
        transform.Translate(transform.forward * Time.deltaTime*v, Space.World);

        transform.rotation = Quaternion.Slerp(from, to, timeCount);
        timeCount = timeCount + Time.deltaTime;
        
        

    }

    void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag != "Bee")
        {
            Rotate();
            v = Random.Range(3f, 10f);
        }
    }



    void Rotate()
    {
        //0 for left, 1 for right
        int dir = Mathf.FloorToInt(Random.Range(0f, 2f));
        if (dir == 0)
        {
            timeCount = 0;
            from = gameObject.transform.rotation;
            Quaternion r = gameObject.transform.rotation;
            r *= Quaternion.Euler(0, 90, 0);
            to = r;

            
        }
        else
        {
            timeCount = 0;
            from = gameObject.transform.rotation;
            Quaternion r = gameObject.transform.rotation;
            r *= Quaternion.Euler(0, -90, 0);
            to = r;
            
        }
    }

    
}
