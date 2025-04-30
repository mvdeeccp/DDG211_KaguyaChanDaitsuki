using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game4Manager : MonoBehaviour
{
    public GameObject modelPanel;
    public GameObject gameplayPanel;
    public GameObject resultPanel;
    public GameObject hintPanel;  
    public TextMeshProUGUI hintText;

    public List<GameObject> modelBeads;    
    public List<GameObject> gameplayBeads;  
    public int totalRounds = 3;

    private int currentRound = 0;
    private int[] hiddenBeadCounts = new int[] { 3, 4, 5 };
    private float[] memoryTimes = new float[] { 3f, 5f, 7f };
    private HashSet<int> hiddenIndexes = new HashSet<int>();

    public GameObject settingsPanel;
    public GameObject tutorialPanel;
    public TextMeshProUGUI finalScoreText;

    public List<GameObject> tutorialPages;
    private int currentTutorialPage = 0;



    void Start()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
        }
        resultPanel.SetActive(false);
        hintPanel.SetActive(false);
        StartCoroutine(ShowModelThenPlay());
    }

    IEnumerator ShowModelThenPlay()
    {
        if (currentRound >= totalRounds)
        {
            ShowResult();
            yield break;
        }

        modelPanel.SetActive(true);
        gameplayPanel.SetActive(false);

        hiddenIndexes.Clear();
        ResetModelBeads();
        ResetGameplayBeads();

        int count = hiddenBeadCounts[currentRound];

        while (hiddenIndexes.Count < count)
        {
            int rand = Random.Range(0, modelBeads.Count);
            hiddenIndexes.Add(rand);
        }

        foreach (int index in hiddenIndexes)
        {
            modelBeads[index].SetActive(false);  
        }

        yield return new WaitForSeconds(memoryTimes[currentRound]);

        modelPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        ResetGameplayBeads();  
    }

    void ResetModelBeads()
    {
        foreach (var bead in modelBeads)
        {
            bead.SetActive(true);
        }
    }

    void ResetGameplayBeads()
    {
        foreach (var bead in gameplayBeads)
        {
            bead.SetActive(true);
        }
    }

    public bool IsCorrect(List<bool> playerRemoved)
    {
        bool isCorrect = true;

        for (int i = 0; i < gameplayBeads.Count; i++)
        {
            bool shouldRemove = hiddenIndexes.Contains(i);
            bool playerDidRemove = playerRemoved[i];

            if (shouldRemove != playerDidRemove)
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            int[] scorePerRound = new int[] { 20, 30, 40 };
            if (currentRound < scorePerRound.Length)
            {
                ScoreManager.Instance.AddScore(scorePerRound[currentRound]);
            }
        }

        currentRound++; 

        if (currentRound >= totalRounds)
        {
            ShowResult();
        }
        else
        {
            StartCoroutine(ShowModelThenPlay());
        }

        return isCorrect;
    }

    void ShowResult()
    {
        gameplayPanel.SetActive(false);
        resultPanel.SetActive(true);

        if (finalScoreText != null)
        {
            finalScoreText.text = $"{ScoreManager.Instance.currentScore}";
        }

    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene("Game5");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleHint()
    {
        if (hintPanel.activeSelf)
        {
            hintPanel.SetActive(false);  
        }
        else
        {
            int totalBeads = modelBeads.Count;
            int hiddenBeads = hiddenBeadCounts[currentRound];

            hintText.text = $"Level {currentRound + 1} : ? / {hiddenBeads}";
            hintPanel.SetActive(true);  
        }
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ToggleTutorialPanel()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(!tutorialPanel.activeSelf);
            if (tutorialPanel.activeSelf)
            {
                currentTutorialPage = 0;
                ShowTutorialPage(currentTutorialPage);
            }
        }
    }

    public void NextTutorialPage()
    {
        if (currentTutorialPage < tutorialPages.Count - 1)
        {
            currentTutorialPage++;
            ShowTutorialPage(currentTutorialPage);
        }
    }

    private void ShowTutorialPage(int index)
    {
        for (int i = 0; i < tutorialPages.Count; i++)
        {
            tutorialPages[i].SetActive(i == index);
        }
    }

}
