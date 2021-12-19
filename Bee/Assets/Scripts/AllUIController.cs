using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllUIController : MonoBehaviour
{
    public GameObject UI, pause, gameOver, popUp, gameDone;

    public static bool isPaused, isDead, isOver;

    public static float width, height, boxHeight, boxWidth;

    RectTransform canvasRect;

    void Start()
    {
        isPaused = isDead = isOver = false;
        canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        setBoxProperties();
    }

    // Update is called once per frame
    void Update()
    {
        setBoxProperties();
        if(HiveScript.allDone)
        {
            GameDone();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isDead && !PopUpScript.popUp)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
        if (isDead || isOver)
        {
            GameOver();
        }
    }

    void setBoxProperties()
    {
        boxHeight = width / 900f * 40f;
        boxWidth = boxHeight * 5f;
        width = canvasRect.sizeDelta.x;
        height = canvasRect.sizeDelta.y;
    }

    void Resume()
    {
        setUI(true, false, false, false, false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        setUI(false, false, true, false, false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void GameOver()
    {
        setUI(false, false, false, false, true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void GameDone()
    {
        Time.timeScale = 0f;
        setUI(false, false, false, true, false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void BackToMenu()
    {
        setUI(true, false, false, false, false);
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void Restart()
    {
        setUI(true, false, false, false, false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void setUI(bool A, bool B, bool C, bool D, bool E)
    {
        //MenuUI.SetActive(X);
        //pauseMenuUI.SetActive(Y);
        //GameOverUI.SetActive(Z);
        UI.SetActive(A);
        popUp.SetActive(B);
        pause.SetActive(C);
        gameDone.SetActive(D);
        gameOver.SetActive(E);
    }
}
