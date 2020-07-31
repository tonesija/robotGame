using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManagerScript : MonoBehaviour
{
    Text timeText;
    Canvas thisCanvas;
    void Start()
    {
        timeText = transform.GetChild(0).GetComponent<Text>();
        thisCanvas = GetComponent<Canvas>();
        thisCanvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = LevelManager.time + "";
    }
}
