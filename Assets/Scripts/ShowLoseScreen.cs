using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowLoseScreen : MonoBehaviour
{
    public static GameObject loseScreen;
    public static GameObject saveButton;
    private void Awake()
    {
        loseScreen = GameObject.Find("LoseScreen");
        saveButton = GameObject.Find("SaveButton");
        loseScreen.SetActive(false);
    }

    public static void SetLoseVisibility(bool value)
    {
        loseScreen.SetActive(value); 
        saveButton.SetActive(!value);
    }

    public void PressAgainButton()
    {
        SetLoseVisibility(false);
        MainMenu.Loading = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
