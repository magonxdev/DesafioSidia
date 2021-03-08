using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    //Classe auxiliar de menu para poder chamar o game over e o menu principal

    public static UIManager instance;

    [SerializeField] private TMP_Text WinnerText;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject MainMenuPanel;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance.gameObject);
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeWinnerText(string txt)
    {
        WinnerText.text = txt;
    }

    public void ShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }

    public void StartGame()
    {
        CameraBattleChanger.instance.StartGameCamera();
        MovementManager.instance.StartGame();
        MainMenuPanel.SetActive(false);
    }

}
