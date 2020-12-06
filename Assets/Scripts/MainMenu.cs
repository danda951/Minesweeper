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
        if (SizeInput.text != "" && MinesInput.text != "" && Int32.Parse(SizeInput.text) > 1 && Int32.Parse(MinesInput.text) > 0 && Int32.Parse(SizeInput.text) <= 10 && (Int32.Parse(SizeInput.text) * Int32.Parse(SizeInput.text) - 2) >= Int32.Parse(MinesInput.text))
        {
            Map.sizeValue = Int32.Parse(SizeInput.text);
            Map.minesValue = Int32.Parse(MinesInput.text);
            GameHandler.diff = GameHandler.Difficulty.Custom;
            Loading = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void GenerateTable()
    {
        HighscoreTable.Generate(GameHandler.diff);
    }

    public void PressLoadButton()
    {
        Loading = true;
        string JsonString = PlayerPrefs.GetString("Save");
        Data data = JsonUtility.FromJson<Data>(JsonString);
        GameHandler.diff = data.diff;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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