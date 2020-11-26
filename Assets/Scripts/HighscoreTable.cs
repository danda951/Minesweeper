using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEditor.UI;

public class HighscoreTable : MonoBehaviour
{
    public enum GeneratingFor
    {
        PostGameTable,
        ShowingTableInMenu
    }
    private static Transform entryTemplate;
    private static Transform entryContainer;
    public static GameObject scoreboard;
    private static List<Transform> transformList;
    private static HS highscores;
    public GeneratingFor genFor;
    private GameHandler.Difficulty MenuTableDiff = GameHandler.Difficulty.Easy;
    public TMP_Text TableNameText;

    public void Awake()
    {
        scoreboard = GameObject.Find("Scoreboard");
        entryContainer = transform.Find("HSEntryContainer");
        entryTemplate = entryContainer.Find("HSEntryTemplate");
        if (genFor == GeneratingFor.ShowingTableInMenu)
        {
            Generate(GameHandler.Difficulty.Easy);
        }
        else
        {
            scoreboard.SetActive(false);
        }
    }

    public static void Generate(GameHandler.Difficulty diff)
    {
        entryTemplate.gameObject.SetActive(false);
        if (diff == GameHandler.Difficulty.Easy)
        {
            string JsonString = PlayerPrefs.GetString("HSTable");
            highscores = JsonUtility.FromJson<HS>(JsonString);
        }
        else if (diff == GameHandler.Difficulty.Medium)
        {
            string JsonString = PlayerPrefs.GetString("HSTable-Medium");
            highscores = JsonUtility.FromJson<HS>(JsonString);
        }
        else if (diff == GameHandler.Difficulty.Hard)
        {
            string JsonString = PlayerPrefs.GetString("HSTable-Hard");
            highscores = JsonUtility.FromJson<HS>(JsonString);
        }

        if (highscores == null)
        {
            highscores = new HS { highScoreEntryList = new List<HighscoreEntry>() };
        }

        for (int i = 0; i < highscores.highScoreEntryList.Count; i++)
        {
            for (int q = 0; q < highscores.highScoreEntryList.Count; q++)
            {
                if (highscores.highScoreEntryList[i].score < highscores.highScoreEntryList[q].score)
                {
                    HighscoreEntry save = highscores.highScoreEntryList[i];
                    highscores.highScoreEntryList[i] = highscores.highScoreEntryList[q];
                    highscores.highScoreEntryList[q] = save;
                }
            }
        }

        if (transformList != null)
        {
            for (int i = 0; i < transformList.Count; i++)
            {
                try
                {
                    Destroy(transformList[i].gameObject);
                }
                catch { }
            }
        }
        transformList = new List<Transform>();

        int count = 0; ;
        if (highscores.highScoreEntryList.Count < 10)
        {
            count = highscores.highScoreEntryList.Count;
        }
        else
        {
            count = 10;
        }

        /*foreach (HighscoreEntry HSE in highscores.highScoreEntryList)
        {
            CreateEntry(HSE, entryContainer, transformList);
        }*/

        for (int i = 0; i < count; i++)
        {
            CreateEntry(highscores.highScoreEntryList[i], entryContainer, transformList);
        }
    }


    public static void AddResult(string name, int score)
    {
        HighscoreEntry highScoreEntry = new HighscoreEntry { name = name, score = score };

        if (GameHandler.diff == GameHandler.Difficulty.Easy)
        {
            string JsonString = PlayerPrefs.GetString("HSTable");
            highscores = JsonUtility.FromJson<HS>(JsonString);
        }
        else if (GameHandler.diff == GameHandler.Difficulty.Medium)
        {
            string JsonString = PlayerPrefs.GetString("HSTable-Medium");
            highscores = JsonUtility.FromJson<HS>(JsonString);
        }
        else if (GameHandler.diff == GameHandler.Difficulty.Hard)
        {
            string JsonString = PlayerPrefs.GetString("HSTable-Hard");
            highscores = JsonUtility.FromJson<HS>(JsonString);
        }

        if (highscores == null)
        {
            highscores = new HS { highScoreEntryList = new List<HighscoreEntry>() };
        }
        highscores.highScoreEntryList.Add(highScoreEntry);

        if (GameHandler.diff == GameHandler.Difficulty.Easy)
        {
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("HSTable", json);
            PlayerPrefs.Save();
        }
        else if (GameHandler.diff == GameHandler.Difficulty.Medium)
        {
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("HSTable-Medium", json);
            PlayerPrefs.Save();
        }
        else if (GameHandler.diff == GameHandler.Difficulty.Hard)
        {
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("HSTable-Hard", json);
            PlayerPrefs.Save();
        }
    }
    private static void CreateEntry(HighscoreEntry scoreEntry, Transform container, List<Transform> transformList)
    {
        float entryHeight = 40f;
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -entryHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = (transformList.Count + 1).ToString();
        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = scoreEntry.name;
        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = scoreEntry.score.ToString();
        transformList.Add(entryTransform);
    }

    public static void SetTableVisibility(bool value)
    {
        scoreboard.SetActive(value);
    }

    public void ResetTable()
    {
        // Reset table code
    }

    public void TableLeftArrow()
    {
        // Left arrow code
    }

    public void TableRightArrow()
    {
        // Right arrow code
    }
}

public class HS
{
    public List<HighscoreEntry> highScoreEntryList;
}

[System.Serializable]
public class HighscoreEntry
{
    public int score;
    public string name;
}