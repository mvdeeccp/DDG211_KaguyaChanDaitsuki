using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject creditPanel;
    public GameObject[] creditPages;
    private int currentPage = 0;

    public void Start()
    {
        creditPanel.SetActive(false);
        optionPanel.SetActive(false);
}
    public void PlayGame()
    {
        SceneManager.LoadScene("Game1");
    }

    public void OpenOption()
    {
        optionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        optionPanel.SetActive(false);
    }

    public void OpenCredit()
    {
        creditPanel.SetActive(true);
        ShowCreditPage(0);
    }

    public void NextCreditPage()
    {
        if (currentPage + 1 < creditPages.Length)
        {
            ShowCreditPage(currentPage + 1);
        }
    }

    public void BackToMenu()
    {
        creditPanel.SetActive(false);
    }

    void ShowCreditPage(int index)
    {
        for (int i = 0; i < creditPages.Length; i++)
        {
            creditPages[i].SetActive(i == index);
        }
        currentPage = index;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
