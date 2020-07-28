using BayatGames.SaveGameFree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager_Script : MonoBehaviour
{
    public Text CurrentLevel;
    public void LevelManager(int i)
    {
        SceneManager.LoadScene(i);
    }
    public List<GameObject> Levels = new List<GameObject>();
    private void Start()
    {
        //SaveGame.DeleteAll();
        if (SaveGame.Exists("CLevel"))
        {
            CurrentLevel.text = SaveGame.Load<int>("CLevel").ToString();
        }
        else
            CurrentLevel.text = "1";
        if (SaveGame.Load<int>("CLevel") == 0)
        {
            CurrentLevel.text = "1";
        }

        int currentLevel = Convert.ToInt32(CurrentLevel.text);
        for (int i = 0; i < currentLevel; i++)
        {
            Levels[i].GetComponent<Button>().enabled = true;
            Levels[i].GetComponent<Image>().color = Color.white;
        }
    }
}
