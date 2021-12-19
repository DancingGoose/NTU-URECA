using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWelcomePage : MonoBehaviour
{
    public GameObject WelcomePage;
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        WelcomePage.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }
}
