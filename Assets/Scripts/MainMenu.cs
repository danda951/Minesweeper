using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField SizeInput;
    public TMP_InputField MinesInput;
    public static bool Loading = false;
    public void PlayGameEasy()
    {
        GameHandler.diff = GameHandler.Difficulty.Easy;
        //Loading = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayGameMedium()
    {
        GameHandler.diff = GameHandler.Difficulty.Medium;
        Loading = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayGameHard()
    {
        GameHandler.diff = GameHandler.Difficulty.Hard;
        Loading = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayGameCustom()
    {
        // Custom game create code
    }

    public void GenerateTable()
    {
        HighscoreTable.Generate(GameHandler.diff);
    }

    public void PressLoadButton()
    {
        // loading button
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}