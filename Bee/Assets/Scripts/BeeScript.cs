using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// SCRIPT TO CONTROL THE BEES
public class BeeScript : MonoBehaviour
{
    /* VARIABLES:
     * 
     * escape indicates if the bee escaping a predator, bringNectar indicates if the bee brings a nectar
     * point indicates the ID of the flower that the bee goes, total counts the total of the flowers, count is helper variable in travel
     * startWaitTime is the default waiting time at the flower, waitTime is the timer, beeSpeed is the default speed of the player
     * noiseX, Y, Z is the noise given to the next position the bee goes, so it looks like the bee moves naturally
     * hivePoint indicates the next hive point the bee goes, hiveSide indicates which side of the hive the bee will go, 
     * state indicates what state is the bee in right now, alternate route indicates the alternate route if the bee wants to go to another flower but not from a flower
     * chasingBird indicates the bird that chase this bee, detector indicates the detector that helps the bee movements
     * birdTrans is the transform of this bee, flowerTarget is the flower the bee wants to go, startPosition is the start position of the bee
     * flows indicates the flows of the bee that goes from the hive to the flower, or vice versa
     */

    public bool escape, bringNectar, inHive;
    public int point, total, count, nectarDelivered;
    private float startWaitTime = 3f, waitTime = 3f, beeSpeed = 20f, noiseX, noiseY, noiseZ, health, maxHealth = 60f;
    public string hivePoint, hiveSide, state;
    string[] alternateRoute;
    GameObject chasingBird;
    public GameObject detector;
    Transform birdTrans;
    Vector3 flowerTarget, startPosition;
    static IDictionary<string, string[]> flows;

    /* FUNCTIONS:
     * 
     * Start is called before the first frame update
     * Update is called once per frame
     * OnTriggerEnter is called when another collider enters this collider
     * OnTriggerExit is called when another collider exits this collider
     * randomNoise is to give a noise to a position bee goes
     * escapingBird is called 1 second after bird starts chasing the bee
     * escapedBird is called after the bee is out of the bird vision range
     * FaceTarget is to face a direction vector3
     * Travel is to decide where to go next
     * Move is to go directly to a point
     * Reset is to change the flower target
     * compareFlowers is to decide which flower to go to
     * goToHive is to go in the right flow from flower to hive
     * goOut is to go in the right flow from hive or flower to flower
     * thinking is to think the positions that the bee need to go
     * goToFlower is to move the bee to the right position in right sequence
     * changeHivePoint is to change the hiveSide or hivePoint
     * noise is to give noise to the next target, except for the flower crown
     * moveFromSomething is to evade an obstacle
     */

    void Start()
    {
        //bee is not escaping, doesn't bring a nectar, and still inside the hive
        escape = bringNectar = false;
        inHive = true;

        //set every integer to 0
        point = total = count = 0;

        health = 60f;

        //set the target flower
        setNewFlowerTarget();

        // set the total
        total = FlowersScript.positions.Count;

        //randomize every bee speed
        beeSpeed = Random.Range(17.5f, 22.5f);

        //it's not being chased
        chasingBird = null;
        
        //set the starting position
        startPosition = transform.position;

        //first state is thinking
        state = "Thinking";

        // set the flows on the left of the hive and on the right of the hive
        flows = new Dictionary<string, string[]>();
        flows.Add("Left", new string[3]);
        flows["Left"][0] = "FrontHalfLeft";
        flows["Left"][1] = "FrontLeft";
        flows["Left"][2] = "BackLeft";
        flows.Add("Right", new string[3]);
        flows["Right"][0] = "FrontHalfRight";
        flows["Right"][1] = "FrontRight";
        flows["Right"][2] = "BackRight";
        alternateRoute = new string[2];

        // add a random noise
        randomNoise();
    }

    // Update is called once per frame
    void Update()
    {
        // change the target flower if the flower is dangerous
        if (FlowersScript.dangerous[point] && !bringNectar)
        {
            setNewFlowerTarget();
            state = "ThinkingAnotherFlower";
        }

        // escape from bird if chased
        if (escape)
            Move(transform.position + transform.position - birdTrans.position);

        // evade from obstacles or other bees if detector detecting something 
        else if (detector.GetComponent<BeeCollisionDetector>().detectSomething)
            moveFromSomething();

        else if (inHive)
            movementInsideHive();

        // go to hive if get the nectar
        else if (bringNectar)
            goToHive();

        // else go to the target flower
        else
            goOut();

        // reduce health
        health -= Time.deltaTime;
        if (health < 0f)
        {
            if (bringNectar)
                HiveScript.nectarNotDelivered += 1;
            Destroy(transform.gameObject);
        }
            
        //else
        //    Debug.Log(transform.name.ToString() + " : " + health.ToString());
    }

    void OnTriggerEnter(Collider other)
    {
        //if being chased by a bird, invoke escaping bird and add the chasing bird
        if (other.tag == "Bird" && chasingBird == null)
        {
            Invoke("escapingBird", 0.5f);
            chasingBird = other.gameObject;
            birdTrans = chasingBird.GetComponent<Transform>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // if escaped the bird, invoke escaped bird
        if (other.gameObject == chasingBird)
        {
            Invoke("escapedBird", 1f);
        }
    }

    void randomNoise()
    {
        noiseX = Random.Range(-3f, 3f);
        noiseY = Random.Range(-3f, 3f);
        noiseZ = Random.Range(-3f, 3f);
    }

    void escapingBird()
    {
        // set escape to true
        escape = true;
    }

    void escapedBird()
    {
        // set escape to false, remove the birds component
        escape = false;
        chasingBird = null;
        birdTrans = null;
    }

    //void Initialize()
    //{
    //    total = FlowersScript.positions.Count;
    //    while (FlowersScript.occupied[point] || FlowersScript.dangerous[point])
    //    {
    //        point = Random.Range(0, total);
    //    }
    //    FlowersScript.occupied[point] = true;
    //    flowerTarget = FlowersScript.positions[point];
    //}

    void movementInsideHive()
    {
        // Thinking is to think the route from the hive to flower
        if (state == "Thinking" || state == "ThinkingAnotherFlower")
        {
            thinking();
        }
        else if (state == "GoToFlower")
        {
            goToFlower();
        }
        // Staying is directly travel to the starting point and stay for waiting time
        else if (state == "Staying")
        {
            Travel(startPosition);
        }
    }

    void goOut()
    {
        //ThinkingAnotherFlower is to think the route from a flower to another flower
        if (state == "ThinkingAnotherFlower")
        {
            thinking();
        }

        // GoToFlower is travel from the hive to flower
        else if (state == "GoToFlower")
        {
            goToFlower();
        }

        // GoToAnotherFlower is travel from a flower to another flower
        else if (state == "GoToAnotherFlower")
        {
            Travel(HiveScript.hivePoints[alternateRoute[count]]);
        }

        // Staying is travel directly to the target flower and stay for waiting time
        else if (state == "Staying")
        {
            Travel(flowerTarget);
        }
    }

    void goToHive()
    {
        // GoToHive is travel back to the hive
        if (state == "GoToHive")
        {
            // if hiveSide is front just go directly to the front, else follow the flow
            if (hiveSide == "Front")
            {
                Travel(HiveScript.hivePoints["Front"]);
            }
            else
            {
                Travel(HiveScript.hivePoints[flows[hiveSide][count]]);
            }
        }

        // Staying is directly travel to the starting point and stay for waiting time
        else if (state == "Staying")
        {
            Travel(startPosition);
        }
    }

    void thinking()
    {
        // deciding which side of the hive is the closest with the target flower
        hiveSide = "Front";
        hiveSide = changeHivePoint(flowerTarget, hiveSide, "Left");
        hiveSide = changeHivePoint(flowerTarget, hiveSide, "Right");
        hiveSide = changeHivePoint(flowerTarget, hiveSide, "Back");

        // set the nearest point from the target flower based on the hive side
        if (hiveSide == "Front")
            hivePoint = "Front";
        if (hiveSide == "Left")
            hivePoint = changeHivePoint(flowerTarget, "FrontLeft", "BackLeft");
        if (hiveSide == "Right")
            hivePoint = changeHivePoint(flowerTarget, "FrontRight", "BackRight");
        if (hiveSide == "Back")
        {
            hivePoint = changeHivePoint(flowerTarget, "BackLeft", "BackRight");
            if (hivePoint == "BackLeft")
                hiveSide = "Left";
            else
                hiveSide = "Right";
        }

        // go to the next state
        state = "GoToFlower";
    }

    void goToFlower()
    {
        // if starts from the front, just directly go to the front point, else follow the flow
        if (hiveSide == "Front")
        {
            Travel(HiveScript.hivePoints["Front"]);
        }
        else
        {
            Travel(HiveScript.hivePoints[flows[hiveSide][count]]);
        }
    }

    void moveFromSomething()
    {
        // set new variable for vertical movement, horizontal movement, and final direction, set all to 0
        Vector3 directionV, directionH, finalDirection;
        directionV = directionH = new Vector3(0f, 0f, 0f);

        // if there's obstacle below, choose up
        if (detector.GetComponent<BeeCollisionDetector>().down)
            directionV = detector.GetComponent<BeeCollisionDetector>().upPosition - transform.position;
            
        // if there's obstacle above
        if (detector.GetComponent<BeeCollisionDetector>().up)
        {
            // if it's still 0, choose below, else revert back to 0 because there's obstacle on both sides
            if (directionV == new Vector3(0f, 0f, 0f))
                directionV = detector.GetComponent<BeeCollisionDetector>().downPosition - transform.position;
            else
                directionV = new Vector3(0f, 0f, 0f);
        }

        // if there's obstacle right, choose left
        if (detector.GetComponent<BeeCollisionDetector>().right)
            directionH = detector.GetComponent<BeeCollisionDetector>().leftPosition - transform.position;

        // if there's obstacle left
        if (detector.GetComponent<BeeCollisionDetector>().left)
        {
            // if it's still 0, choose right, else revert back to 0 because there's obstacle on both sides
            if (directionH == new Vector3(0f, 0f, 0f))
                directionH = detector.GetComponent<BeeCollisionDetector>().rightPosition - transform.position;
            else
                directionH = new Vector3(0f, 0f, 0f);
        }

        // sum up the bee position, vertical and horizontal direction
        finalDirection = transform.position + directionH + directionV;

        
        if (finalDirection == transform.position)
            Travel(-transform.forward);
        else
            Travel(finalDirection);
            
        //Travel(Vector3.forward);
        //if (finalDirection == transform.position)
        //{
        //    FaceTarget(detector.GetComponent<BeeCollisionDetector>().rightPosition);
        //    FaceTarget(detector.GetComponent<BeeCollisionDetector>().leftPosition);
        //    FaceTarget(detector.GetComponent<BeeCollisionDetector>().upPosition);
        //    FaceTarget(detector.GetComponent<BeeCollisionDetector>().downPosition);
        //}
            
    }

    void FaceTarget(Vector3 V)
    {
        // set the vector rotation
        Vector3 direction = (V - transform.position).normalized;

        //if equals to 0, no need to rotate
        if (direction != new Vector3(0f, 0f, 0f))
        {
            // rotate transform step by step
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void Travel(Vector3 V)
    {
        // enter only if the distance of the target if below 1f
        if (Vector3.Distance(transform.position, V) < 1f)
        {
            // staying, directly to the flower or hive
            if (state == "Staying")
            {
                // wait time is over
                if (waitTime <= 0)
                {
                    // if bring nectar (in the hive), increase nectar delivered, set bring nectar to false, change state to thinking again
                    if (bringNectar)
                    {
                        HiveScript.nectarDelivered++;
                        nectarDelivered += 1;
                        Debug.Log(transform.name);
                        ScoreScript.Scores[transform.name] += 1;
                        bringNectar = false;
                        state = "Thinking";
                    }

                    // if the flower has a nectar, bring a nectar, change hive side, remove the nectar, and change state to go to hive
                    else if (FlowersScript.transforms[point].GetComponent<FlowerScript>().nectar)
                    {
                        bringNectar = true;
                        if (hiveSide == "Back")
                        {
                            hiveSide = changeHivePoint(flowerTarget, "Right", "Left");
                            count = 2;
                        }
                        FlowersScript.transforms[point].GetComponent<FlowerScript>().removeNectar();
                        health += Vector3.Distance(FlowersScript.positions[point], HiveScript.position) / 10f;
                        if (health > maxHealth)
                            health = maxHealth;
                        state = "GoToHive";
                    }

                    // if the flower doesn't have a nectar
                    else if(!bringNectar)
                    {
                        state = "ThinkingAnotherFlower";
                    }

                    // change the rotation to V
                    transform.LookAt(V);

                    //choose another target flower
                    setNewFlowerTarget();
                }

                //else reduce the time
                else
                    waitTime -= Time.deltaTime;
            }

            // go to the flower
            else if (state == "GoToFlower")
            {
                // change the state to Staying if it's the last second point, else go to the next point
                if (hivePoint == "Front")
                {
                    randomNoise();
                    state = "Staying";
                }
                else if (flows[hiveSide][count] == hivePoint)
                {
                    randomNoise();
                    state = "Staying";
                }
                else
                    count += 1;
            }

            // go back to hive
            else if (state == "GoToHive")
            {
                // change the state to Staying if it's the last second point, else go to the next point, else go to next point
                if (count == 0)
                {
                    randomNoise();
                    state = "Staying";
                }
                else
                    count -= 1;
            }

            // go to another flower
            else if (state == "GoToAnotherFlower")
            {
                // change the state to Staying if it's the last second point, else go to the next point, else go to next point
                if (alternateRoute[count] == hivePoint)
                {
                    randomNoise();
                    state = "Staying";
                }
                else
                    count += 1;
            }
        }

        //else go to the next point
        else
        {
            Move(V);
        }
    }

    void Move(Vector3 V)
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        FaceTarget(V);
        transform.position = Vector3.MoveTowards(transform.position, V, beeSpeed * Time.deltaTime);
    }

    void setNewFlowerTarget()
    {
        // reset the waiting time
        waitTime = startWaitTime;

        // change the occupied flower, also change the target flower
        FlowersScript.occupied[point] = false;
        point = compareFlowers(point);
        FlowersScript.occupied[point] = true;
        flowerTarget = FlowersScript.positions[point];
    }

    int compareFlowers(int temp)
    {
        // helper variable a
        int a = 0;
        
        // distances note in dictionary so we easily sort in use the keys and value
        IDictionary<int, float> distances = new Dictionary<int, float>();
        IDictionary<int, float> sortedDistances = new Dictionary<int, float>();

        // note the distances in the dictionary variables
        foreach (Vector3 v in FlowersScript.positions)
        {
            distances.Add(a, Vector3.Distance(transform.position, v));
            a += 1;
        }

        // sort the values in new dictionary
        foreach (KeyValuePair<int, float> distance in distances.OrderBy(key => key.Value))
        {
            sortedDistances.Add(distance);
        }

        // copy the key values from sorted distances to a new list
        var sortedPoints = sortedDistances.Keys.ToList();

        // go to a nearest flower where it's not occuped, not dangerous, with nectar
        foreach (int p in sortedPoints)
        {
            if (!(p == temp) && !FlowersScript.occupied[p] && !FlowersScript.dangerous[p] && FlowersScript.nectars[p])
                return p;
        }

        // if can not find, go to a nearest flower where it's not occuped, either dangerous or not, with nectar
        foreach (int p in sortedPoints)
        {
            if (!(p == temp) && !FlowersScript.occupied[p] && FlowersScript.nectars[p])
                return p;
        }

        // tries to find a nearest flower where it's not occupied and not dangerous
        while (true)
            foreach (int p in sortedPoints)
            {
                if (!(p == temp) && !FlowersScript.occupied[p] && !FlowersScript.dangerous[p])
                    return p;
            }
    }

    string changeHivePoint(Vector3 v, string s1, string s2)
    {
        // set the two string
        string A = s1, B = s2;
        
        // return a string with the shortest distance
        if (Vector3.Distance(v, HiveScript.hivePoints[s1]) < Vector3.Distance(v, HiveScript.hivePoints[s2]))
            return A;
        else
            return B;
    }

    Vector3 noise(Vector3 v)
    {
        Vector3 newV = v;
        newV.x += noiseX;
        newV.y += noiseY;
        newV.z += noiseZ;
        return newV;
    }
}
