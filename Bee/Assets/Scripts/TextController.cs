using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
   public void ShowText (string msg)
    {
        transform.GetComponent<Text>().text = msg;
    }
}
