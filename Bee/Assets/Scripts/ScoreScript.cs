using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public GameObject scores, title, MenuButton, RestartButton, bees;
    Transform beesTransform;
    public static IDictionary<string, int> Scores, sortedScores;
    public static TextMeshProUGUI titleText, scoresText, menuText, restartText;
    RectTransform titleRect, scoresRect, menuButtonRect, restartButtonRect;
    string stringScores;

    // Start is called before the first frame update
    void Start()
    {
        Scores = new Dictionary<string, int>();
        sortedScores = new Dictionary<string, int>();
        beesTransform = bees.GetComponent<Transform>();
        titleRect = title.GetComponent<RectTransform>();
        scoresRect = scores.GetComponent<RectTransform>();
        menuButtonRect = MenuButton.GetComponent<RectTransform>();
        restartButtonRect = RestartButton.GetComponent<RectTransform>();
        titleText = title.GetComponent<TextMeshProUGUI>();
        scoresText = scores.GetComponent<TextMeshProUGUI>();
        menuText = MenuButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        restartText = RestartButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        foreach(Transform bee in beesTransform)
        {
            Scores.Add(bee.name, 0);
            Debug.Log(bee.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        setBoxProperties();
        Scores["BeePlayer"] = PlayerControlScript.nectarDelivered;
        sortedScores = new Dictionary<string, int>();
        foreach (KeyValuePair<string, int> distance in Scores.OrderByDescending(key => key.Value))
        {
            sortedScores.Add(distance);
        }
        stringScores = "";
        foreach (KeyValuePair<string, int> bee in sortedScores)
        {
            stringScores += "\n" + bee.Key + " - " + bee.Value.ToString();
        }
        scoresText.text = stringScores;
    }

    void setBoxProperties()
    {
        titleRect.sizeDelta = new Vector2(0.9f * AllUIController.width, 0.625f * AllUIController.height);
        titleText.fontSize = AllUIController.width / 900f * 60f;
        menuText.fontSize = AllUIController.width / 900f * 20f;
        restartText.fontSize = AllUIController.width / 900f * 20f;
        scoresText.fontSize = AllUIController.width / 900f * 20f;
        menuButtonRect.position = new Vector2(AllUIController.width / 2f, AllUIController.height * 0.25f + 2 * AllUIController.boxHeight);
        restartButtonRect.position = new Vector2(AllUIController.width / 2f, AllUIController.height * 0.25f);
        scoresRect.sizeDelta = new Vector2(0.9f * AllUIController.width, 0.625f * AllUIController.height);
        menuButtonRect.sizeDelta = new Vector2(AllUIController.boxWidth, AllUIController.boxHeight);
        restartButtonRect.sizeDelta = new Vector2(AllUIController.boxWidth, AllUIController.boxHeight);

    }

    public static void printScore()
    {
        
        foreach (KeyValuePair<string,int> bee in sortedScores)
        {
            scoresText.text += "\n" + bee.Key + " - " + bee.Value.ToString();
        }
    }

    public static void addBee(string name, int score)
    {
        Scores.Add(name, score);
        
        foreach (KeyValuePair<string, int> distance in Scores.OrderBy(key => key.Value))
        {
            sortedScores.Add(distance);
        }
        printScore();
    }
}
