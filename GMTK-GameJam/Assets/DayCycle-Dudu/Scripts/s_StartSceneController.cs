using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class s_StartSceneController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Awake() 
    {        
        startButton.onClick.AddListener(() => {
            SceneManager.LoadScene("DuduScene");
        });   
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });   
    }
}
