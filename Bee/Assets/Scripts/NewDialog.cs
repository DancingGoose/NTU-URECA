using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;


public class NewDialog : MonoBehaviour
{
    public CharController mifeng;
    public TextController content;
    public string[] strsContent = File.ReadAllLines(@"E:\A-Learning\UnityLearn\New Unity Project\Assets\Materials\content.txt");
    public string[] strsName = File.ReadAllLines(@"E:\A-Learning\UnityLearn\New Unity Project\Assets\Materials\name.txt");
    int index = 0;
    public GameObject bg;
    public GameObject hive;
    
    void Awake()
    {
        bg.SetActive(false);
    }
    void Update()
    {
        if (bg.activeSelf == true && Input.GetMouseButtonDown(0))
        {
            
            if (index < strsContent.Length)
            {
                mifeng.LoadChar(strsName[index]);
                content.ShowText(strsContent[index]);
                index++;
            }
            else
            {
                bg.SetActive(false);
             }
        }
    }
}
