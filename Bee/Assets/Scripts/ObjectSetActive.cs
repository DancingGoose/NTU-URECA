using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSetActive : MonoBehaviour
{
    public GameObject bg;
    // Start is called before the first frame update
    void Start()
    {
        bg.SetActive(false);
    }

}
