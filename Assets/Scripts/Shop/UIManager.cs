using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public Text DiamondText;
    public Image SelectionUI;
    public Text[] Amount;
    public static UIManager _instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("Your UI Manager instance is null");
            }
            return instance;
        }
    }

    public void SeletionUIPos(float yPos)
    {
        SelectionUI.rectTransform.anchoredPosition = new Vector2(SelectionUI.rectTransform.anchoredPosition.x,yPos);
    }
    public void OpenShop(int diamond)
    {
        DiamondText.text = diamond + "G";
    }
    private void Awake()
    {
        instance = this;
    }

    public void Exit_to_DashBoard()
    {
        SceneManager.LoadScene(11);
    }
}
