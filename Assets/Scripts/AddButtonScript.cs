using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AddButtonScript : MonoBehaviour
{
    public static GameObject addField;
    public TMP_InputField name_field;
    private void Awake()
    {
        addField = GameObject.Find("AddNewHighscore");
        addField.SetActive(false);
    }

    public static void SetAddVisibility(bool value)
    {
        addField.SetActive(value);
    }

    public void PressAddButton()
    {
        addField.SetActive(false);
        HighscoreTable.AddResult(name_field.text, TextStatsUpdate.timeText);
        HighscoreTable.Generate(GameHandler.diff);
        HighscoreTable.SetTableVisibility(true);
    }
}
