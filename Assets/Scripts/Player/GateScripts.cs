using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GateScripts : MonoBehaviour
{
    [SerializeField]
    public Text PlayerKeyText;
    [SerializeField]
    public GameObject Panel;

    public static GateScripts gateScripts;
    public static GateScripts _gateScripts
    {
        get
        {
            if (gateScripts == null)
            {

            }
            return gateScripts;
        }

    }
    private void Start()
    {
        gateScripts = this;
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            if (PlayerKeyText.text == "1")
            {
                // Noe Will Happen
            }
            else
            {
                Panel.SetActive(true);
                return;
            }

            int currentScene = SceneManager.GetActiveScene().buildIndex;

            if (currentScene == 10)
            {
                SceneManager.LoadScene(0);
                SaveGame.Save<int>("CLevel", 10);
                return;
            }
            SceneManager.LoadScene(currentScene + 1);

            bool exits = SaveGame.Exists("CLevel") ? true : false;
            if (exits == false)
            {
                SaveGame.Save<int>("CLevel", currentScene + 1);
            }
            if (SaveGame.Load<int>("CLevel") == 10)
                return;
            else
                SaveGame.Save<int>("CLevel", currentScene + 1);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            Panel.SetActive(false);
        }

    }
}
