using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MapGridObject
{
    public enum Type // Enum který slouží k určení typu daného hracího pole
    {
        Empty, // Prázdné
        Mine, // Obsahuje minu
        MineNum_1, // 1 mina v okolí
        MineNum_2, // ...
        MineNum_3,
        MineNum_4,
        MineNum_5,
        MineNum_6,
        MineNum_7,
        MineNum_8,
    }

    private Grid<MapGridObject> grid; // Grid ve kterém je buňka umístěna
    private int x; // Pozice X v matici
    private int y; // Pozice Y v matici
    public Type type { set; get; } // Její typ
    public bool isFlagged { set; get; } = false; // Jestli je označená vlajkou
    public bool isRevealed { set; get; } = false; // Jestli již je odhalena ("rozkliknuta")
    private GameHandler gameHandler;
    private AudioManagerScript audioManager;
    public GameObject flagTexture { set; get; } // Textura praporu
    public GameObject coverTexture { set; get; } // Textura zakrytí
    public GameObject ownTexture { set; get; } // Textura pod pokrývkou
    public GameObject backgroundTexture { get; set; } // Pozadí buňky

    public MapGridObject(Grid<MapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        GameObject thePlayer = GameObject.Find("MinesweeperGameHandler");
        gameHandler = thePlayer.GetComponent<GameHandler>();

        GameObject m = GameObject.Find("AudioManager");
        audioManager = m.GetComponent<AudioManagerScript>();
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
        if (isFlagged && isRevealed != true)
        {
            audioManager.PlayRemoveFlagSound();
            isFlagged = false;
            gameHandler.TextureDel(flagTexture);
            TextStatsUpdate.flagText++;
        }
        else if (!isFlagged && isRevealed != true && TextStatsUpdate.flagText > 0)
        {
            audioManager.PlayAddFlagSound();
            isFlagged = true;
            flagTexture = gameHandler.Render(RenderTexture(), gameHandler.FlagTexture);
            TextStatsUpdate.flagText--;
        }
        grid.TriggerGridObjectChanged(x, y);
    }

    public void CheckAround(Map map)
    {
        MapGridObject[,] gridArray = map.GetGrid().GetArray();
        if ((x + 1) < map.GetGrid().GetWidth() && gridArray[x + 1, y].type != MapGridObject.Type.Mine)
        {
            gridArray[x + 1, y].LeftClick(map);
        }

        if ((x - 1) >= 0 && gridArray[x - 1, y].type != MapGridObject.Type.Mine)
        {
            gridArray[x - 1, y].LeftClick(map);
        }

        if ((y + 1) < map.GetGrid().GetHeight() && gridArray[x, y + 1].type != MapGridObject.Type.Mine)
        {
            gridArray[x, y + 1].LeftClick(map);
        }

        if ((y - 1) >= 0 && gridArray[x, y - 1].type != MapGridObject.Type.Mine)
        {
            gridArray[x, y - 1].LeftClick(map);
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
        if (type == Type.Mine && isFlagged != true)
        {
            audioManager.PlayExplosionFX();
            map.GameOver(Map.EndType.Lose);
        }

        if (isRevealed == false && isFlagged == false && type != Type.Empty && type != Type.Mine)
        {
            audioManager.PlayRemoveSingleCoverFX();
            isRevealed = true;
            gameHandler.TextureDel(coverTexture);
            gameHandler.LowerCovered();
        }

        else if (isRevealed == false && isFlagged == false && type == Type.Empty)
        {
            audioManager.PlayRemoveMultipleCoverFX();
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