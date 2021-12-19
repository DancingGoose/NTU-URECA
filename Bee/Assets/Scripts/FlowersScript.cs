using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SCRIPT TO HELP BEES DOING THE ALGORITHM
public class FlowersScript : MonoBehaviour
{
    /* VARIABLES:
     * 
     * count is to give ID to each of the flowers
     * list of occupied, dangerous, nectars is the current situation of each of the flower
     * list of transforms id to get the positions of each flowers
     * list of oldpositions is the middle vectors of each flowers, meanwhile list of positions is the position of the flower crown
     */
    public static int count;
    public static List<bool> occupied, dangerous, nectars;
    public static List<Transform> transforms;
    public static List<Vector3> oldPositions, positions;

    /* FUNCTIONS:
     * 
     * Awake is called after all objects are initialized
     * Update is called once per frame
     * Normalizer is to change the middle vector of the flower to the crown position
     */

    void Awake()
    {
        // initialize everything to 0 or new
        count = 0;
        occupied = new List<bool>();
        dangerous = new List<bool>();
        nectars = new List<bool>();
        oldPositions = new List<Vector3>();
        positions = new List<Vector3>();
        transforms = new List<Transform>();

        // get the object that contains the flowers
        GameObject flowers = GameObject.Find("Flowers");
        Transform flowerPositions = flowers.GetComponent<Transform>();

        // goes through each of the flowers
        foreach (Transform child in flowerPositions)
        {
            // add the middle positions and transforms
            oldPositions.Add(child.position);
            transforms.Add(child);

            // give the ID to each of the flowers
            child.GetComponent<FlowerScript>().ID = count;
            
            //add the conditions for each flowers
            occupied.Add(false);
            dangerous.Add(false);
            nectars.Add(true);

            //count increases so the ID of each flowers is different
            count += 1;
        }

        // go through each middle positions and add the crown positions
        foreach (Vector3 v in oldPositions)
        {
            positions.Add(Normalizer(v));
        }
    }

    Vector3 Normalizer(Vector3 V)
    {
        Vector3 newV = V;
        newV.y += 5.5f;
        newV.x -= 4.75f;
        return newV;
    }
}
