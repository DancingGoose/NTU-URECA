using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentController : MonoBehaviour
{
    public Material skyOne;
    public Material skyTwo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
        if (Input.GetKeyDown(KeyCode.E))
        {
            RenderSettings.skybox = skyOne;
            Debug.Log("E pressed");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            RenderSettings.skybox = skyTwo;
            Debug.Log("Q pressed");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SceneOne");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("SceneTwo");
        }
    }
}
