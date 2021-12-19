using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    GameObject[] textObj = new GameObject[5];
    Slider healthBar;
    TextMeshProUGUI[] text = new TextMeshProUGUI[5];
    RectTransform[] rectTrans = new RectTransform[8];
    Vector3 midVector3;
    float healthBarWidth;

    void Start()
    {
        textObj[0] = GameObject.Find("Nectar");
        textObj[1] = GameObject.Find("Nectar Delivered");
        textObj[2] = GameObject.Find("Fly");
        textObj[3] = GameObject.Find("UV View");
        textObj[4] = GameObject.Find("Timer");
        for(int i = 0; i<5; i++)
        {
            text[i] = textObj[i].GetComponent<TextMeshProUGUI>();
            rectTrans[i] = textObj[i].GetComponent<RectTransform>();
        }
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        rectTrans[5] = GameObject.Find("HealthBar").GetComponent<RectTransform>();
        rectTrans[6] = healthBar.transform.Find("Border").GetComponent<RectTransform>();
        healthBarWidth = 200f + AllUIController.boxHeight / 2f; 
        midVector3 = rectTrans[5].position;
    }

    void Update()
    {
        float boxWidth = AllUIController.boxWidth;
        float boxHeight = AllUIController.boxHeight;
        text[0].text = "Nectar: " + PlayerControlScript.bringNectar.ToString();
        text[1].text = "Nectar Delivered: " + PlayerControlScript.nectarDelivered.ToString();
        text[2].text = "Fly: " + PlayerControlScript.fly.ToString();
        text[3].text = "UV View: " + PlayerControlScript.UVview.ToString();
        text[4].text = Mathf.Ceil(PlayerControlScript.timer).ToString();
        for (int i = 0; i < 5; i++)
        {
            text[i].fontSize = AllUIController.width / 900f * 15f;
        }
        rectTrans[0].position = transformText(-AllUIController.width / 2f + boxWidth, AllUIController.height / 2f - boxHeight);
        rectTrans[1].position = transformText(-AllUIController.width / 2f + boxWidth, -AllUIController.height / 2f + boxHeight);
        rectTrans[2].position = transformText(AllUIController.width / 2f - boxWidth, -AllUIController.height / 2f + boxHeight);
        rectTrans[3].position = transformText(AllUIController.width / 2f - boxWidth, AllUIController.height / 2f - boxHeight);
        rectTrans[4].position = transformText(0, AllUIController.height / 2f - boxHeight);
        rectTrans[5].position = transformText(-AllUIController.width / 2f + boxWidth, AllUIController.height / 2f - 2 * boxHeight);
        //healthBar.transform.position = transformText(-AllUIController.width / 2f + boxWidth, AllUIController.height / 2f - 2*boxHeight);
        //rectTrans[5].sizeDelta = new Vector2(AllUIController.width / 3.8f, AllUIController.height / 40f);
        rectTrans[5].localScale = new Vector3(AllUIController.height / 600f * 1.5f, AllUIController.height / 600f * 2f, 1f);
        //rectTrans[6].sizeDelta = new Vector2(AllUIController.height / 600f * 2f, AllUIController.height / 600f * 8f);
        healthBar.value = PlayerControlScript.health;
    }

    Vector3 transformText(float X, float Y)
    {
        Vector3 Z = new Vector2(AllUIController.width/2f, AllUIController.height/2f);
        //Debug.Log(Z.ToString() + " " + width.ToString() + " " + height.ToString());
        Z.x += X;
        Z.y += Y;
        return Z;
    }
}
