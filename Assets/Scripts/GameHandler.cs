using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using System.Data;
using System.Transactions;
using System.Linq.Expressions;
using System;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public enum GameStatus
    {
        Preparing,
        Running,
        Stopped,
    }
    public enum Difficulty
    {
        Medium,
        Easy,
        Hard,
        Custom,
    }

    public Map map { private get; set; }
    public GameObject UnClicked_Block_Texture;
    public GameObject FlagTexture;
    public GameObject MineTexture;
    public GameObject EmptyTexture;
    public TextMeshProUGUI FlagTextObject;
    public TextMeshProUGUI TimeTextObject;
    public GameObject[] NumbersTexture = new GameObject[8];
    private GameStatus gameStatus;
    public static Difficulty diff = Difficulty.Easy;

    public void LowerCovered()
    {
        map.Covered--;
    }
    public void SetGameStatus(GameStatus gameStat)
    {
        gameStatus = gameStat;
    }
    public GameStatus GetGameStatus()
    {
        return gameStatus;
    }

    private void Start()
    {
        gameStatus = GameStatus.Preparing;
        map = new Map();
        map.GenegrateMap();
    }

    private void Update()
    {
        if (gameStatus == GameStatus.Running)
        {
            if (map.Covered == map.Mines && TextStatsUpdate.flagText == 0)
            {
                map.GameOver(Map.EndType.Win);
            }

            if (Input.GetMouseButtonDown(0))
            {
                try
                {
                    map.GetGrid().GetValue(GetMouseWorldPosition()).LeftClick(map);
                }
                catch { }
            }

            if (Input.GetMouseButtonDown(1))
            {
                try
                {
                    map.GetGrid().GetValue(GetMouseWorldPosition()).RightClick();
                }
                catch { }
            }
        }
    }

    public GameObject Render(Vector3 vector, GameObject texture)
    {
        return Instantiate(texture, vector, Quaternion.identity);
    }

    public void TextureDel(GameObject texture)
    {
        Destroy(texture);
    }

    public void PressSaveButton()
    {
        Data data = new Data();
        data.TypeList = new List<MapGridObject.Type>();
        data.RevealedList = new List<bool>();
        data.FlaggedList = new List<bool>();
        for (int x = 0; x < map.size; x++)
        {
            for (int y = 0; y < map.size; y++)
            {
                data.TypeList.Add(map.GetGrid().GetArray()[x, y].type);
                data.RevealedList.Add(map.GetGrid().GetArray()[x, y].isRevealed);
                data.FlaggedList.Add(map.GetGrid().GetArray()[x, y].isFlagged);
            }
        }
        data.Mines = map.Mines;
        data.Size = map.size;
        data.diff = diff;
        data.time = TextStatsUpdate.timer + 1.0f;

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Save", json);
        PlayerPrefs.Save();
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

}

public class Data
{
    public List<MapGridObject.Type> TypeList;
    public List<bool> RevealedList;
    public List<bool> FlaggedList;
    public int Mines;
    public int Size;
    public GameHandler.Difficulty diff;
    public float time;
}
