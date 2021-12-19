using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("doorPassThrough", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name + " enters the door ");
    }

    private void OnTriggerExit(Collider other) {
        score++;
        PlayerPrefs.SetInt("doorPassThrough", score);
        Debug.Log(other.gameObject.name + " exits the door ");
        Debug.Log(other.gameObject.name + " pass through the door " + PlayerPrefs.GetInt("doorPassThrough") + " times");
    }
}
