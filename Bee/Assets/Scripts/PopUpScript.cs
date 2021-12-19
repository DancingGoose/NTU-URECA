using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject title, information, OkButton, popUpUI;
    public TextMeshProUGUI titleText, informationText, okText;
    public Image bg;
    RectTransform canvas, titleRect, informationRect, OkButtonRect;
    bool[] conditions;
    public static bool popUp;
    int N = 6;
    void Start()
    {
        bg = popUpUI.GetComponent<Image>();
        setBox(false);
        titleText = title.GetComponent<TextMeshProUGUI>();
        informationText = information.GetComponent<TextMeshProUGUI>();
        okText = OkButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        Invoke("Begin", 1f);
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        titleRect = title.GetComponent<RectTransform>();
        informationRect = information.GetComponent<RectTransform>();
        OkButtonRect = OkButton.GetComponent<RectTransform>();
        setBoxProperties();
        popUp = false;
        conditions = new bool[N];
        for (int i = 0; i < N; i++)
        {
            conditions[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        setBoxProperties();
        if (conditions[5])
        {

        }
        else if (conditions[4])
        {
            if (!PlayerControlScript.bringNectar)
            {
                Invoke("nectarOff", 0.5f);
                conditions[5] = true;
            }
        }
        else if (conditions[3])
        {
            if (HiveScript.playerInside)
            {
                Invoke("insideOfHive", 1f);
                conditions[4] = true;
            }
        }
        else if (conditions[2])
        {
            if (PlayerControlScript.bringNectar)
            {
                nectarIn();
            }
        }
        else if (conditions[1])
        {
            if (PlayerControlScript.UVview)
            {
                UV();
            }
        }
        else if (conditions[0])
        {
            if(!HiveScript.playerInside)
            {
                outOfHive();
            }
        }
    }

    void setBoxProperties()
    {
        titleRect.sizeDelta = new Vector2(0.9f * AllUIController.width, 1.1f * AllUIController.height); // chenjun edit 11/12
        titleRect.position = new Vector2(AllUIController.width / 2, 180f);
        informationRect.sizeDelta = new Vector2(0.7f * AllUIController.width, 1.1f * AllUIController.height);
        //canvas.sizeDelta = new Vector2(0.75f , 0.75f);
        titleText.fontSize = AllUIController.width / 900f * 60f;
        informationText.fontSize = AllUIController.width / 900f * 30f;
        informationRect.position = new Vector2(AllUIController.width / 2, 140);

        okText.fontSize = AllUIController.width / 900f * 15f;
        OkButtonRect.position = new Vector2(AllUIController.width /2f, AllUIController.height * 0.175f);

        //bg.rectTransform.sizeDelta = new Vector2(0.9f * AllUIController.width, 0.8f * AllUIController.height);
    }

    void Begin()
    {
        setText("Welcome",
                "\nWelcome to the game! You are now a bee! In this game we learn how does a bee interract with its world.\n" +
                "\nTo control the bee, use WASD and mouse to move. Press SPACE to fly. RIGHT CLICK to accelarate.\n" +
                "\nThe health of the bee is decreasing every time. To gain more health, you should take some nectars!\n" 
                );
        activateBox(0);
    }

    void outOfHive()
    {
        setText("You are outside the hive",
                "\n\nWow! look at the world! It is the same as what human see\n" +
                "\nBut as a bee, you can look through the UV light\n" +
                "\nPress F to use the UV view!");
        activateBox(1);
    }

    void UV()
    {
        setText("You activated the UV view!",
                "\n\nAs you can see, we are using UV view to detect the flower that still has nectars\n" +
                "\nNow you should go to the flower that still some nectars!\n" +
                "\nIt is indicated by the purple particle effect");
        activateBox(2);
    }
    void nectarIn()
    {
        setText("You have taken the nectar!",
                "\n\nCongratulation! you have taken the nectar.\n" +
                "\nIn addition you now have additional HP\n" +
                "\nNow let's return the bee hive");
        activateBox(3);
    }

    void insideOfHive()
    {
        setText("You are inside the hive",
                "\n\nNow, press left click to put the nectar!");
        activateBox(4);
    }

    void nectarOff()
    {
        setText("You have put the nectar!",
                "\n\nCongratulation! you have put the nectar.\n" +
                "\nNow let's continue to fill up the hive with nectar from other flowers!\n");
        activateBox(5);
    }

    void activateBox(int X)
    {
        Time.timeScale = 0f;
        setBox(true);
        setMouse(true);
        conditions[X] = true;
        popUp = true;
    }

    public void OK()
    {
        Time.timeScale = 1f;
        setBox(false);
        setMouse(false);
        popUp = false;
    }

    void setBox(bool X)
    {
        popUpUI.SetActive(X);
        //title.SetActive(X);
        //information.SetActive(X);
        //OkButton.SetActive(X);
        //transform.GetComponent<Image>().enabled = X;
    }

    void setText(string T, string I)
    {
        titleText.text = T;
        informationText.text = I;
    }

    void setMouse(bool X)
    {
        AllUIController.isPaused = X;
        Cursor.visible = X;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
