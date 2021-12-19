using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    public void LoadChar (string imgName)
    {
        Sprite sp = Resources.Load<Sprite>(imgName);
        transform.GetComponent<Image>().sprite = sp;
    }
}
