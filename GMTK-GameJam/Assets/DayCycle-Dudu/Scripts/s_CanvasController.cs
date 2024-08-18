using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class s_CanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI eventText;
    [SerializeField] private Image clockImage;
    [SerializeField] private Color dayClockColor;
    [SerializeField] private Color nightClockColor;

    private void Start() 
    {
        GameController.Instance.OnDayNightChanged += GameController_OnDayNightChanged;    
    }

    private void Update() 
    {
        clockImage.fillAmount = GameController.Instance.GetTimeReason();   
    }

    private void GameController_OnDayNightChanged(object sender, EventArgs e)
    {
        if(GameController.Instance.GetIsDay())
        {
            clockImage.color = dayClockColor;

            eventText.text = "The sun is rising up...";
            eventText.transform.LeanScale(Vector3.one, 1f);
            eventText.transform.LeanScale(Vector3.zero, 1f).setDelay(3f);
        }
        else
        {
            clockImage.color = nightClockColor;

            eventText.text = "The night comes...";
            eventText.transform.LeanScale(Vector3.one, 1f);
            eventText.transform.LeanScale(Vector3.zero, 1f).setDelay(3f);
        }
    }
}
