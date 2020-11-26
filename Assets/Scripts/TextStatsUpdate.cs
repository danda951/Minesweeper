using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextStatsUpdate : MonoBehaviour
{
    public static int timeText { private set; get; } = 0;
    public static int flagText {set; get; }
    public static float timer { private set; get; } = 0.0f;
    private GameHandler gameHandler;

    private void Start()
    {
        GameObject thePlayer = GameObject.Find("MinesweeperGameHandler");
        gameHandler = thePlayer.GetComponent<GameHandler>();
        if (GameHandler.diff == GameHandler.Difficulty.Easy)
        {
            flagText = 3;
        }
        else if(GameHandler.diff == GameHandler.Difficulty.Medium)
        {
            flagText = 10;
        }
        else if (GameHandler.diff == GameHandler.Difficulty.Hard)
        {
            flagText = 15;
        }
    }

    private void Update()
    {
        ;
    }
    public static void LoadTime(float time)
    {
        timer = time;
    }
}