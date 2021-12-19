using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartMenuPopUp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject title, information, OkButton, popUpUI, mainUI;
    public TextMeshProUGUI titleText, informationText, okText;
    RectTransform canvas, titleRect, informationRect, OkButtonRect;
    public static bool popUp;
    float width, height;
    void Start()
    {
        titleText = title.GetComponent<TextMeshProUGUI>();
        informationText = information.GetComponent<TextMeshProUGUI>();
        okText = OkButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        titleRect = title.GetComponent<RectTransform>();
        informationRect = information.GetComponent<RectTransform>();
        OkButtonRect = OkButton.GetComponent<RectTransform>();
        setBoxProperties();
        popUpUI.SetActive(false);
        mainUI.SetActive(true);
        popUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        setBoxProperties();
    }

    void setBoxProperties()
    {
        width = canvas.sizeDelta.x;
        height = canvas.sizeDelta.y;
        titleRect.sizeDelta = new Vector2(0.9f * width, 0.75f * height);//Chenjun changed 0.75 to 0.25
        informationRect.sizeDelta = new Vector2(0.9f * width, 0.75f * height);
        titleText.fontSize = width / 900f * 60f;
        informationText.fontSize = width / 900f * 30f;
        okText.fontSize = width / 900f * 15f;
        OkButtonRect.position = new Vector2(width / 2f, height * 0.175f);
    }

    public void Settings()
    {
        activateBox();
        setText("Controls",
                "\nWASD and mouse - movement\n" +
                "\nSpace - fly\n" +
                "\nLeft Click - Take or put nectars\n" +
                "\nRight Click - Accelerate");
    }

    void activateBox()
    {
        popUpUI.SetActive(true);
        mainUI.SetActive(false);
        popUp = true;
    }

    public void OK()
    {
        Time.timeScale = 1f;
        popUpUI.SetActive(false);
        mainUI.SetActive(true);
        popUp = false;
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
