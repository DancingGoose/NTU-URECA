using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuScript : MonoBehaviour
{
    public GameObject Title, PlayButton, SettingsButton, QuitButton;

    RectTransform canvasRect, TitleRect, PlayButtonRect, SettingsButtonRect, QuitButtonRect;

    TextMeshProUGUI TitleText, PlayText, SettingsText, QuitText;

    float width, height, boxHeight, boxWidth;

    private void Start()
    {
        canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        TitleRect = Title.GetComponent<RectTransform>();
        PlayButtonRect = PlayButton.GetComponent<RectTransform>();
        SettingsButtonRect = SettingsButton.GetComponent<RectTransform>();
        QuitButtonRect = QuitButton.GetComponent<RectTransform>();
        TitleText = Title.GetComponent<TextMeshProUGUI>();
        PlayText = PlayButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        SettingsText = SettingsButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        QuitText = QuitButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        setBoxProperties();
    }

    private void Update()
    {
        setBoxProperties();
    }

    void setBoxProperties()
    {
        width = canvasRect.sizeDelta.x;
        height = canvasRect.sizeDelta.y;
        boxHeight = width / 900f * 40f;
        boxWidth = boxHeight * 5f;
        TitleRect.sizeDelta = new Vector2(0.9f * width, 0.5f * height);
        //PlayButtonRect.position = new Vector2(width / 2f, height * 0.25f + 2 * boxHeight);
        //SettingsButtonRect.position = new Vector2(width / 2f, height * 0.25f + boxHeight);
        //QuitButtonRect.position = new Vector2(width / 2f, height * 0.25f);
        PlayButtonRect.position = new Vector2(width / 2f, height/2f);
        SettingsButtonRect.position = new Vector2(width / 2f - 1.25f * boxWidth, height/2f);
        QuitButtonRect.position = new Vector2(width / 2f + 1.25f * boxWidth, height/2f);
        PlayButtonRect.sizeDelta = new Vector2(boxWidth, boxHeight);
        SettingsButtonRect.sizeDelta = new Vector2(boxWidth, boxHeight);
        QuitButtonRect.sizeDelta = new Vector2(boxWidth, boxHeight);
        TitleText.fontSize = width / 900f * 60f;
        PlayText.fontSize = width / 900f * 20f;
        SettingsText.fontSize = width / 900f * 20f;
        QuitText.fontSize = width / 900f * 20f;
    }

    public void ChangeScene(string X)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(X);
    }

    public void btnGoogle()
    {
        Application.OpenURL("www.google.com");
    }

    public void btnQuit()
    {
        Application.Quit();
    }
}
