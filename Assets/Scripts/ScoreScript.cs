using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public int score;
    public bool scoreFound;
    public string givenName;
    public TMP_InputField input;
    public GameObject returnButton;
    public Transform scoresTransform;
    public TransitionScript changeScene;

    void Start()
    {
        Summarise();
        if (score >= PlayerPrefs.GetInt("highscore_points7"))
        {
            input.characterLimit = 5;
            ShuffleScores("?????");
        }
        else
        {
            input.transform.parent.gameObject.SetActive(false);
            returnButton.SetActive(true);
        }
    }

    private void Awake()
    {
        PopulateData();
        PrintScores();
        changeScene.ChooseCursor("menuCrosshair");
    }

    private void Update()
    {
        input.ActivateInputField();
    }
    public void Summarise()
    {
        try
        {
            score = WinCheck.Instance.score;
        }
        catch (Exception ex)
        {
            Debug.Log(ex, this);
            score = 45000;
        }
    }    

    public void PopulateData()
    {
        if (PlayerPrefs.HasKey("donePopulating").Equals(false))
        {
            PlayerPrefs.SetString("highscore_name0", "PABLO");
            PlayerPrefs.SetString("highscore_name1", "JACEK");
            PlayerPrefs.SetString("highscore_name2", "FAVOR");
            PlayerPrefs.SetString("highscore_name3", "XABCX");
            PlayerPrefs.SetString("highscore_name4", "XDEFX");
            PlayerPrefs.SetString("highscore_name5", "XGHIX");
            PlayerPrefs.SetString("highscore_name6", "XJKLX");
            PlayerPrefs.SetString("highscore_name7", "XMNOX");
            PlayerPrefs.SetString("highscore_name8", "XPRSX");
            PlayerPrefs.SetInt("highscore_points0", 80000);
            PlayerPrefs.SetInt("highscore_points1", 70000);
            PlayerPrefs.SetInt("highscore_points2", 60000);
            PlayerPrefs.SetInt("highscore_points3", 50000);
            PlayerPrefs.SetInt("highscore_points4", 40000);
            PlayerPrefs.SetInt("highscore_points5", 30000);
            PlayerPrefs.SetInt("highscore_points6", 20000);
            PlayerPrefs.SetInt("highscore_points7", 10000);
            PlayerPrefs.SetInt("highscore_points8", 0);

            PlayerPrefs.SetString("donePopulating", "yes");
        }
    }

    public void SubmitName()
    {
        scoreFound = false;
        givenName = input.text;
        if (!givenName.Equals(""))
        {
            ShuffleScores(givenName);
            input.transform.parent.gameObject.SetActive(false);
            returnButton.SetActive(true);
        }

    }

    public void PrintScores()
    {
        for (int i=0; i < 8; i++)
        {
            scoresTransform.GetChild(i).GetComponent<TMP_Text>().text = PlayerPrefs.GetString("highscore_name" + i);
            scoresTransform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("highscore_points" + i).ToString("D6");
        }
    }

    public void ShuffleScores(string name)
    {
        for (int i=0; i < 8; i++)
        {
            if (score >= PlayerPrefs.GetInt("highscore_points" + i) && !scoreFound)
            {
                if (!PlayerPrefs.GetString("highscore_name" + i).Equals("?????"))
                {
                    for (int j = 0; j < 8 - i; j++)
                    {
                        PlayerPrefs.SetInt("highscore_points" + (8 - j), PlayerPrefs.GetInt("highscore_points" + (7 - j)));
                        PlayerPrefs.SetString("highscore_name" + (8 - j), PlayerPrefs.GetString("highscore_name" + (7 - j)));
                    }
                }
                PlayerPrefs.SetInt("highscore_points" + i, score);
                PlayerPrefs.SetString("highscore_name" + i, name);
                scoreFound = true;
            }
        }

        PrintScores();
    }

    public void ThisIsTheEnd()
    {
         score = 0;
         PlayerPrefs.SetInt("currentScore", score);
         changeScene.MainMenu();
    }
}
