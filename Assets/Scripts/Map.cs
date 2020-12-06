using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Jobs;
using CodeMonkey.Utils;
using System.Threading;
using UnityEngine.Rendering;
using UnityEditor;

public class Map
{
    public Grid<MapGridObject> grid;
    private static GameHandler gameHandler;
    private List<MapGridObject> RevealedGridObjects = new List<MapGridObject>();
    public static int minesValue;
    public int Mines;
    public static int sizeValue;
    public int size;
    public int Covered;

    public List<MapGridObject> GetRevealedObjects()
    {
        return RevealedGridObjects;
    }

    public void AddRevealedObject(MapGridObject ob)
    {
        RevealedGridObjects.Add(ob);
    }
    public enum EndType
    {
        Win,
        Lose,
    }
    public Map()
    {
        GameObject thePlayer = GameObject.Find("MinesweeperGameHandler");
        gameHandler = thePlayer.GetComponent<GameHandler>();

        if (GameHandler.diff == GameHandler.Difficulty.Easy)
        {
            size = 5;
            Mines = 3;
        }
        else if (GameHandler.diff == GameHandler.Difficulty.Medium)
        {
            size = 10;
            Mines = 10;
        }
        else if (GameHandler.diff == GameHandler.Difficulty.Hard)
        {
            size = 10;
            Mines = 15;
        }
        else if (GameHandler.diff == GameHandler.Difficulty.Custom)
        {
            size = sizeValue;
            Mines = minesValue;
        }

        grid = new Grid<MapGridObject>(size, size, 100f, (10 - size) * new Vector3(50, 0), (Grid<MapGridObject> g, int x, int y) => new MapGridObject(g, x, y));
        Covered = grid.GetWidth() * grid.GetWidth();
    }

    public Grid<MapGridObject> GetGrid()
    {
        return grid;
    }

    public void SetSprite(Vector3 worldPosition, MapGridObject.Type typ)
    {
        MapGridObject ob = grid.GetValue(worldPosition);
        if (ob != null)
        {
            ob.SetSprite(typ);
        }
    }
    public void GenegrateMap()
    {
        MapGridObject[,] gridArray = grid.GetArray();
        if (MainMenu.Loading == false)
        {
            int mines = Mines;
            while (mines > 0)
            {
                int x = Random.Range(0, grid.GetWidth());
                int y = Random.Range(0, grid.GetHeight());
                if (gridArray[x, y].GetSprite() != MapGridObject.Type.Mine)
                {
                    gridArray[x, y].SetSprite(MapGridObject.Type.Mine);
                    mines--;
                }
            }
        }
        else
        {
            string JsonString = PlayerPrefs.GetString("Save");
            Data data = JsonUtility.FromJson<Data>(JsonString);
            int mines = data.Mines;
            size = data.Size;
            TextStatsUpdate.LoadTime(data.time);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (data.TypeList[y + (x * size)] == MapGridObject.Type.Mine)
                    {
                        gridArray[x, y].SetSprite(MapGridObject.Type.Mine);
                        mines--;
                    }
                }
            }
        }

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                int counter = 0;
                if (gridArray[x, y].GetSprite() != MapGridObject.Type.Mine)
                {
                    if (x < (grid.GetWidth() - 1) && gridArray[x + 1, y].GetSprite() == MapGridObject.Type.Mine) counter++;
                    if (x > 0 && gridArray[x - 1, y].GetSprite() == MapGridObject.Type.Mine) counter++;
                    if (y < (grid.GetHeight() - 1) && gridArray[x, y + 1].GetSprite() == MapGridObject.Type.Mine) counter++;
                    if (y > 0 && gridArray[x, y - 1].GetSprite() == MapGridObject.Type.Mine) counter++;
                    if (x < (grid.GetWidth() - 1) && y < (grid.GetHeight() - 1) && gridArray[x + 1, y + 1].GetSprite() == MapGridObject.Type.Mine) counter++;
                    if (x < (grid.GetWidth() - 1) && y > 0 && gridArray[x + 1, y - 1].GetSprite() == MapGridObject.Type.Mine) counter++;
                    if (x > 0 && y < (grid.GetHeight() - 1) && gridArray[x - 1, y + 1].GetSprite() == MapGridObject.Type.Mine) counter++;
                    if (x > 0 && y > 0 && gridArray[x - 1, y - 1].GetSprite() == MapGridObject.Type.Mine) counter++;

                    switch (counter)
                    {
                        case 0:
                            gridArray[x, y].SetSprite(MapGridObject.Type.Empty);
                            break;
                        case 1:
                            gridArray[x, y].SetSprite(MapGridObject.Type.MineNum_1);
                            gridArray[x, y].backgroundTexture = (gameHandler.Render(gridArray[x, y].RenderTexture(), gameHandler.NumbersTexture[0]));
                            break;
                        case 2:
                            gridArray[x, y].SetSprite(MapGridObject.Type.MineNum_2);
                            gridArray[x, y].backgroundTexture = (gameHandler.Render(gridArray[x, y].RenderTexture(), gameHandler.NumbersTexture[1]));
                            break;
                        case 3:
                            gridArray[x, y].SetSprite(MapGridObject.Type.MineNum_3);
                            gridArray[x, y].backgroundTexture = (gameHandler.Render(gridArray[x, y].RenderTexture(), gameHandler.NumbersTexture[2]));
                            break;
                        case 4:
                            gridArray[x, y].SetSprite(MapGridObject.Type.MineNum_4);
                            gridArray[x, y].backgroundTexture = (gameHandler.Render(gridArray[x, y].RenderTexture(), gameHandler.NumbersTexture[3]));
                            break;
                        case 5:
                            gridArray[x, y].SetSprite(MapGridObject.Type.MineNum_5);
                            gridArray[x, y].backgroundTexture = (gameHandler.Render(gridArray[x, y].RenderTexture(), gameHandler.NumbersTexture[4]));
                            break;
                        case 6:
                            gridArray[x, y].SetSprite(MapGridObject.Type.MineNum_6);
                            gridArray[x, y].backgroundTexture = (gameHandler.Render(gridArray[x, y].RenderTexture(), gameHandler.NumbersTexture[5]));
                            break;
                        case 7:
                            gridArray[x, y].SetSprite(MapGridObject.Type.MineNum_7);
                            gridArray[x, y].backgroundTexture = (gameHandler.Render(gridArray[x, y].RenderTexture(), gameHandler.NumbersTexture[6]));
                            break;
                        case 8:
                            gridArray[x, y].SetSprite(MapGridObject.Type.MineNum_8);
                            gridArray[x, y].backgroundTexture = (gameHandler.Render(gridArray[x, y].RenderTexture(), gameHandler.NumbersTexture[7]));
                            break;
                    }
                }
            }
        }

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                gridArray[x, y].coverTexture = gameHandler.Render(gridArray[x, y].RenderTexture(), gameHandler.UnClicked_Block_Texture);
                gridArray[x, y].backgroundTexture = gameHandler.Render(gridArray[x, y].RenderTexture(), gameHandler.EmptyTexture);

                if (gridArray[x, y].GetSprite() == MapGridObject.Type.Mine)
                {
                    gridArray[x, y].ownTexture = gameHandler.Render(gridArray[x, y].RenderTexture(), gameHandler.MineTexture);
                }
            }
        }

        gameHandler.SetGameStatus(GameHandler.GameStatus.Running);

        if (MainMenu.Loading == true)
        {
            string JsonString = PlayerPrefs.GetString("Save");
            Data data = JsonUtility.FromJson<Data>(JsonString);
            Debug.Log("Loading saved data!");
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (data.RevealedList[y + (x * size)] == true)
                    {
                        gridArray[x, y].LeftClick(this);
                    }

                    if (data.FlaggedList[y + (x * size)] == true)
                    {
                        gridArray[x, y].RightClick();
                        Debug.Log("Loading saved data!");
                    }
                }
            }
        }
    }

    public void RevealColumn(int x)
    {
        if (x >= 0 && x < grid.GetWidth())
        {
            MapGridObject[,] gridArray = grid.GetArray();
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                gameHandler.TextureDel(gridArray[y, x].coverTexture);
                grid.TriggerGridObjectChanged(y, x);
            }
        }
    }
    public void GameOver(EndType endType)
    {
        gameHandler.SetGameStatus(GameHandler.GameStatus.Stopped);
        if (endType == EndType.Lose)
        {
            Debug.Log("GAME OVER!");
            ShowLoseScreen.SetLoseVisibility(true);
            for (int x = 0; x < grid.GetWidth(); x++)
            {
                RevealColumn(x);
            }
        }
        else if (endType == EndType.Win)
        {
            AddButtonScript.SetAddVisibility(true);
            Debug.Log("YOU WON!");
        }
        // UtilsClass.CreateWorldText("Konec Hry!", null,  new Vector3(0, 50), 100, Color.white, TextAnchor.MiddleCenter);
    }
}
