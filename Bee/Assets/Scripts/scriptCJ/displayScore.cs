using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayScore : MonoBehaviour
{

    TextMeshProUGUI mText;
    
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponent<TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
        mText.text = "0";
        mText.fontSize = AllUIController.width / 900f * 60f;
        rect.position = new Vector2(0.9f * AllUIController.width, 0.7f * AllUIController.height);
    }

    // Update is called once per frame
    void Update()
    {
        mText.text =  "PlayerControlScript.nectarDelivered";
        
    }
}
