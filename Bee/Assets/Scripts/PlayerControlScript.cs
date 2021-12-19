using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SCRIPT TO CONTROL THE BEE AND THE HUMAN
public class PlayerControlScript : MonoBehaviour
{
    /* VARIABLES:
     * 
     * beeMode true if player is a bee, otherwise player is a human, bringNectar indicates bring a nectar
     * fly indicates if it flies, UVview indicates if the player uses the UV view
     * nectarDelivered counts how many nectars you delivered to the hive
     * speedH and speedV is the speed of the mouse tilt 
     * yaw and pitch is the angel position of the character 
     * health is the current health, maxHealth is the maximum value of the health
     * timer is the countdown for the level
     * normalSpeed is the normal speed of the, playerSpeed is the speed that will be updated if an action is done
     * playerObj is the object of the player, could be the bee or the human, depends on action
     * postProcessing is where the UV View will be handled 
     * playerRigidBody and playerTransfom are depend on the playerObj
     * because it will automatically move after the change to human, it will have the beePlayerScript
     * beeColliders
     */

    public static bool beeMode, bringNectar, fly, UVview;
    public static int nectarDelivered;
    public static float speedH = 2.0f, speedV = 2.0f, yaw = 0.0f, pitch = 0.0f, health, maxHealth = 60f, timer, normalSpeed = 20, playerSpeed;
    public static GameObject playerObj;
    public GameObject postProcessing;
    Rigidbody playerRigidBody;
    Transform playerTransform;
    BeeScript beePlayerScript;
    Collider[] beeColliders;

    /* FUNCTIONS:
     * 
     * Start is called before the first frame update
     * Update is called once per frame
     * changeBody is to change the player body to bee or human
     * autoPilotBee is to de/activate the BeeScript when goes to human mode
     */

    void Start()
    {
        //we start with bee mode, which doesn't bring nectar, fly, and active the uv view
        beeMode = true;
        bringNectar = fly = UVview = false;

        //need to set this numbers inside the start because every restart we need to initialize this numbers, if not it wil stay like the previous session
        timer = 600f;
        health = 60f;
        nectarDelivered = 0;
        playerSpeed = 20;

        // set the player object, transform, rigidbosy, bee script, and colliders
        playerObj = GameObject.Find("BeePlayer");
        playerTransform = playerObj.GetComponent<Transform>();
        playerRigidBody = playerObj.GetComponent<Rigidbody>();
        beePlayerScript = playerObj.GetComponent<BeeScript>();
        beeColliders = playerObj.GetComponents<Collider>();
    }

    void Update()
    {
        // run if it's not paused and timer not over
        if (!AllUIController.isPaused && !AllUIController.isOver)
        {
            // W to move forward or S to move backward
            if (Input.GetKey(KeyCode.W))
                playerTransform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.Self);
            else if (Input.GetKey(KeyCode.S))
                playerTransform.Translate(Vector3.back * Time.deltaTime * playerSpeed, Space.Self);

            // D to move right or A to move left
            if (Input.GetKey(KeyCode.D))
                playerTransform.Translate(Vector3.right * Time.deltaTime * playerSpeed, Space.Self);
            else if (Input.GetKey(KeyCode.A))
                playerTransform.Translate(Vector3.left * Time.deltaTime * playerSpeed, Space.Self);

            // E to change from bee to human and vice versa
            if (Input.GetKeyDown(KeyCode.E))
            {
                beeMode = !beeMode;
                if (beeMode)
                    changeBody("BeePlayer");
                else
                    changeBody("Person");
            }

            // some actions will be available when you are in a bee mode
            if (beeMode)
            {
                // right click to speed up the bee, but the health will decrease quick, else it will be normal
                if (Input.GetMouseButton(1))
                {
                    playerSpeed = 2.5f * normalSpeed;
                    health -= 3 * Time.deltaTime;
                }
                else
                {
                    playerSpeed = normalSpeed;
                    health -= Time.deltaTime;
                }

                // F to change to UV View or to normal View
                if (Input.GetKeyDown(KeyCode.F))
                {
                    UVview = !UVview;
                    postProcessing.SetActive(UVview);
                    if (UVview)
                        LightScript.changeLight(1);
                    else
                        LightScript.changeLight(0);
                }

                // space activate fly or deactivate fly
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerRigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                    fly = !fly;
                    playerRigidBody.useGravity = !fly;
                }
            }
            
            // updates the camera tilt
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            // set the pitch only goes between -30 and 30
            if (pitch > 30)
            {
                pitch = 30;
            }
            else if (pitch < -30)
            {
                pitch = -30;
            }

            // don't change the pitch when in human mode, it will look weird
            if (beeMode)
                playerTransform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            else
                playerTransform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
            
            // if the health after addition goes beyond max health, set it to max health
            if (health > maxHealth)
                health = maxHealth;
            
            // bee dies when health is below zero
            if (health < 0)
            {
                autoPilotBee(true);
                AllUIController.isOver = true;
            }

            // timer controls, game over when timer is run out
            if (timer > 0)
                timer -= Time.deltaTime;
            else if (timer < 0)
                timer = 0;
            else
            {
                autoPilotBee(true);
                AllUIController.isOver = true;
            }        
        }
    }

    void changeBody(string body)
    {
        // update the camera, transform, and rigidbody
        CameraMode.playerObj = playerObj = GameObject.Find(body);
        playerTransform = playerObj.GetComponent<Transform>();
        playerRigidBody = playerObj.GetComponent<Rigidbody>();
        
        // player change to human
        if (body == "Person")
        {
            // deactivate UV view
            UVview = false;
            postProcessing.SetActive(UVview);
            LightScript.changeLight(0);

            // set the bee to autopilot
            Rigidbody BeeRigidBody = GameObject.Find("BeePlayer").GetComponent<Rigidbody>();
            BeeRigidBody.useGravity = false;
            autoPilotBee(true);
        }
        // player change to bee. deactivate the autopilot
        else
            autoPilotBee(false); 
    }

    void autoPilotBee(bool X)
    {
        beePlayerScript.enabled = X;
        foreach (Collider collider in beeColliders)
        {
            collider.enabled = !X;
        }
    }
}
