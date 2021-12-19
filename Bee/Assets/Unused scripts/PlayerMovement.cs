using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //applying bullet
    
    Rigidbody playerRigidBody;
    float bulletSpeed = 1100;
    float playerSpeed = 10;
    public GameObject bullet;

    //applying mouse movement

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = transform.GetComponent<Rigidbody>();
    }

    void Fire()
    {
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 1f);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.W))
        {
            // playerRigidBody.AddForce(playerRigidBody.transform.forward * playerSpeed);
            transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.Self);
            // Vector3 playerPosition = transform.position;
            // playerPosition.z += 0.05f;
            // transform.position = playerPosition;
            // transform.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            // playerRigidBody.AddForce(playerRigidBody.transform.forward * playerSpeed * -1);
            transform.Translate(Vector3.back * Time.deltaTime * playerSpeed, Space.Self);
            // Vector3 playerPosition = transform.position;
            // playerPosition.z -= 0.05f;
            // transform.position = playerPosition;
            // transform.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(Vector3.back);
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed, Space.Self);
            // Vector3 playerPosition = transform.position;
            // playerPosition.x += 0.05f;
            // transform.position = playerPosition;
            // transform.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(Vector3.right);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * playerSpeed, Space.Self);
            // Vector3 playerPosition = transform.position;
            // playerPosition.x -= 0.05f;
            // transform.position = playerPosition;
            // transform.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(Vector3.left);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRigidBody.AddForce(new Vector3(0,10,0), ForceMode.VelocityChange);
            //also can apply this to movement
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                string assetName = hit.transform.name;
                Debug.Log(assetName);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Fire();
        }

        yaw += speedH * Input.GetAxis("Mouse X");
        // pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        
    }


}
