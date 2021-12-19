using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beePollenSoundPlay : MonoBehaviour
{

    public AudioClip score;
    public AudioClip bird;
    public GameObject bird1;
    public GameObject bird2;
    AudioSource audiosource;
    AudioSource audioSourceForScore;
    public GameObject scoreAudioHolder;
    float distance1;
    float distance2;
    const float offset = 80;
    bool birdSoundStartState = false;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audioSourceForScore = scoreAudioHolder.GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        distance1 = Vector3.Distance(bird1.transform.position, transform.position);
        distance2 = Vector3.Distance(bird2.transform.position, transform.position);
        if (distance1 < offset || distance2 < offset)
        {
            
            if (birdSoundStartState == false)
            {
                Debug.Log("nearBird, Play sound");
                audiosource.clip = bird;
                audiosource.Play(0);
                birdSoundStartState = true;
            }
        }
        if (distance1 > offset && distance2 > offset)
        {
            audiosource.Stop();
            birdSoundStartState = false;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        
        if (other.gameObject.tag == "pollen")
        {
            Debug.Log("score");
            
            audioSourceForScore.Play(0);
        }
        
    }

    
}
