using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SCRIPT TO CONTROL ONE FLOWER
public class FlowerScript : MonoBehaviour
{
    /* VARIABLES:
     * 
     * nectar is the nectar avability of the flower
     * ID is the unique ID of the flower
     * crownPosition gives the position above the crown of the flower
     * playerObj is the player current object
     * particle gives the effect of glowing if it still has a nectar
     */

    public bool nectar;
    public int ID;
    GameObject playerObj;
    public GameObject particle;
    Vector3 crownPosition;

    /* FUNCTIONS:
     * 
     * Start is called before the first frame update
     * Update is called once per frame
     * removeNectar is to remove the nectar, can be called from bee script
     */

    void Start()
    {
        // flower have nectar in the beginning
        nectar = true;

        // set the crown position
        crownPosition = transform.Find("Particle").position;
        crownPosition.y += 3f;

        // set the player object
        playerObj = GameObject.Find("BeePlayer");
    }

    void Update()
    {
        // de/activate the partcile based on uv view from player control script
        particle.SetActive(PlayerControlScript.UVview);

        // player can take the nectar by left click only if the player is nearby, does not bring a nectar, activate the UV view, and the flower still has the nectar
        if (Vector3.Distance(playerObj.transform.position, crownPosition) < 3 && 
            !PlayerControlScript.bringNectar && PlayerControlScript.UVview && nectar)
        {
            //remove nectar, set the player's bring nectar to true, and increase the health of the player
            removeNectar();
            PlayerControlScript.bringNectar = true;
            PlayerControlScript.health += Vector3.Distance(crownPosition, HiveScript.position) / 10f;
        }
    }

    public void removeNectar()
    {
        // destroy the particle
        ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
        Destroy(particle);

        //set the nectar and the nectars[ID] to false
        FlowersScript.nectars[ID] = nectar = false;
    }
}
