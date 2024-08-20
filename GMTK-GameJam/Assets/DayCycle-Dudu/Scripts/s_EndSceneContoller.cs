using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class s_EndSceneContoller : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    private void Awake() 
    {
        mainMenuButton.onClick.AddListener(() => {
            SceneManager.LoadScene("StartScene");
        });     
    }
}
