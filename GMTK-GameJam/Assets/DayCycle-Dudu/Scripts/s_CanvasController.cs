using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class s_CanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI eventText;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private Image clockImage;
    [SerializeField] private Color dayClockColor;
    [SerializeField] private Color nightClockColor;
    [SerializeField] private GameObject pauseScreen;

    private void Start() 
    {
        GameController.Instance.OnDayNightChanged += GameController_OnDayNightChanged;  
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        GameController.Instance.isPaused = !GameController.Instance.isPaused;
        pauseScreen.SetActive(GameController.Instance.isPaused);

        if(GameController.Instance.isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
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

    public void DisplayText(string text)
    {
        displayText.text = text;

        displayText.transform.LeanScale(Vector3.one, 1f);
        displayText.transform.LeanScale(Vector3.zero, 1f).setDelay(3f);
    }
}
