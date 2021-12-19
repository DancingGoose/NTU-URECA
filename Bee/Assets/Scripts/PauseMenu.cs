using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI, MenuUI, GameOverUI, pauseTitle, MenuButton, RestartButton;

    RectTransform canvas, pauseRect, menuButtonRect, restartButtonRect;

    TextMeshProUGUI pauseText, MenuText, RestartText;

    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        pauseRect = pauseTitle.GetComponent<RectTransform>();
        menuButtonRect = MenuButton.GetComponent<RectTransform>();
        restartButtonRect = RestartButton.GetComponent<RectTransform>();
        MenuText = MenuButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        RestartText = RestartButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        pauseText = pauseTitle.GetComponent<TextMeshProUGUI>();
        setBoxProperties();
    }

    // Update is called once per frame
    void Update()
    {
        setBoxProperties();
    }

    void setBoxProperties()
    {
        pauseRect.sizeDelta = new Vector2(0.9f * AllUIController.width, 0.625f * AllUIController.height);
        pauseRect.position = new Vector2(AllUIController.width / 2f, AllUIController.height / 2f -20);
        pauseText.fontSize = AllUIController.width / 900f * 60f;
        MenuText.fontSize = AllUIController.width / 900f * 20f;
        RestartText.fontSize = AllUIController.width / 900f * 20f;
        //menuButtonRect.position = new Vector2(AllUIController.width / 2f, AllUIController.height * 0.25f + 2 * AllUIController.boxHeight);
        //restartButtonRect.position = new Vector2(AllUIController.width / 2f, AllUIController.height * 0.25f);
        menuButtonRect.position = new Vector2( AllUIController.width/2f-150f, AllUIController.height / 2f-40 );
        restartButtonRect.position = new Vector2(AllUIController.width / 2f + 150f, AllUIController.height / 2f-40);
        menuButtonRect.sizeDelta = new Vector2(AllUIController.boxWidth, AllUIController.boxHeight);
        restartButtonRect.sizeDelta = new Vector2(AllUIController.boxWidth, AllUIController.boxHeight);
        
    }

}
