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

    void Start()
    {
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
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene("NextLevelSceneName");
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
}
