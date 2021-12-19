using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    NavMeshAgent nav;
    
    // Start is called before the first frame update
    void Start()
    {
        nav = GameObject.Find("Body").GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                nav.SetDestination(hit.point);
            }
        }        
    }
}
