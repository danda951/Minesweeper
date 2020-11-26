using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MapGridObject
{
    public enum Type
    {
        Empty,
        Mine,
        MineNum_1,
        MineNum_2,
        MineNum_3,
        MineNum_4,
        MineNum_5,
        MineNum_6,
        MineNum_7,
        MineNum_8,
    }

    private Grid<MapGridObject> grid;
    private int x;
    private int y;
    public Type type { set; get; }
    public bool isFlagged { set; get; } = false;
    public bool isRevealed { set; get; } = false;
    private GameHandler gameHandler;
    public GameObject flagTexture { set; get; }
    public GameObject coverTexture { set; get; }
    public GameObject ownTexture { set; get; }
    public GameObject backgroundTexture { get; set; }

    public MapGridObject(Grid<MapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        GameObject thePlayer = GameObject.Find("MinesweeperGameHandler");
        gameHandler = thePlayer.GetComponent<GameHandler>();
    }

    public void SetSprite(Type typ)
    {
        type = typ;
        grid.TriggerGridObjectChanged(x, y);
    }

    public Type GetSprite()
    {
        return type;
    }
    public void RightClick()
    {
        if(isFlagged && isRevealed != true)
        {
            isFlagged = false;
            gameHandler.TextureDel(flagTexture);
            TextStatsUpdate.flagText++;
        }
        else if(!isFlagged && isRevealed != true && TextStatsUpdate.flagText > 0)
        {
            isFlagged = true;
            flagTexture = gameHandler.Render(RenderTexture(), gameHandler.FlagTexture);
            TextStatsUpdate.flagText--;
        }
        grid.TriggerGridObjectChanged(x, y);
    }

    public void CheckAround(Map map)
    {
        MapGridObject[,] gridArray = map.GetGrid().GetArray();
        if ((x+1) < map.GetGrid().GetWidth() && gridArray[x+1, y].type != MapGridObject.Type.Mine)
        {
            gridArray[x + 1, y].LeftClick(map);
        }
        
        if ((x - 1) >= 0 && gridArray[x-1, y].type != MapGridObject.Type.Mine)
        {
            gridArray[x - 1, y].LeftClick(map);
        }
        
        if ((y + 1) < map.GetGrid().GetHeight() && gridArray[x, y+1].type != MapGridObject.Type.Mine)
        {
            gridArray[x, y+1].LeftClick(map);
        }
        
        if ((y - 1) >= 0 && gridArray[x, y-1].type != MapGridObject.Type.Mine)
        {
            gridArray[x, y-1].LeftClick(map);
        }

        if ((x + 1) < map.GetGrid().GetWidth() && (y + 1) < map.GetGrid().GetHeight() && gridArray[x + 1, y + 1].type != MapGridObject.Type.Mine)
        {
            gridArray[x + 1, y + 1].LeftClick(map);
        }
        if ((x - 1) >= 0 && (y - 1) >= 0 && gridArray[x - 1, y - 1].type != MapGridObject.Type.Mine)
        {
            gridArray[x - 1, y - 1].LeftClick(map);
        }
        if ((x + 1) < map.GetGrid().GetWidth() && (y - 1) >= 0 && gridArray[x + 1, y - 1].type != MapGridObject.Type.Mine)
        {
            gridArray[x + 1, y - 1].LeftClick(map);
        }
        if ((x - 1) >= 0 && (y + 1) < map.GetGrid().GetHeight() && gridArray[x - 1, y + 1].type != MapGridObject.Type.Mine)
        {
            gridArray[x - 1, y + 1].LeftClick(map);
        }

    }
    public void LeftClick(Map map)
    {
        if(type == Type.Mine && isFlagged != true)
        {
            map.GameOver(Map.EndType.Lose);
        }
        
        if(isRevealed == false && isFlagged == false && type != Type.Empty && type != Type.Mine)
        {
            isRevealed = true;
            gameHandler.TextureDel(coverTexture);
            gameHandler.LowerCovered();
        }

        else if(isRevealed == false && isFlagged == false && type == Type.Empty)
        {
            isRevealed = true;
            gameHandler.TextureDel(coverTexture);
            gameHandler.LowerCovered();
            CheckAround(map);
        }
        else
        {
            return;
        }
        grid.TriggerGridObjectChanged(x, y);
    }

    public Vector3 RenderTexture()
    {
        return (grid.GetWorldPosition(x, y) + new Vector3(grid.GetCellSize(), grid.GetCellSize()) * .5f);
    }

    public override string ToString()
    {
        if (isFlagged) 
        {
            return type.ToString() + "\nFLAG";
        }
        else
        {
            return type.ToString();
        }
    }
}