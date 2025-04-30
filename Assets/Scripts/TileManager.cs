using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    private Camera _camera;
    [SerializeField] private PuzzleTile[] tiles;
    private int emptySpaceIndex = 15;
    private bool _isFinished;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TextMeshProUGUI endPanelTimeText;

    [SerializeField] private GameObject PrototypePanel;
    [SerializeField] private GameObject image1;
    [SerializeField] private GameObject image2;
    private int clickCount = 0;

    [SerializeField] private TextMeshProUGUI endPanelScoreText;

    
    public GameObject settingsPanel;
    public GameObject tutorialPanel;

    public List<GameObject> tutorialPages = new List<GameObject>();
    private int currentTutorialPage = 0;



    private void Start()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
        }
        _camera = Camera.main;
        endPanel.SetActive(false);
        Shuffle();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 2)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    PuzzleTile thisTile = hit.transform.GetComponent<PuzzleTile>();
                    emptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePosition;
                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
        }
        if (!_isFinished)
        {
            int correctTiles = 0;
            foreach (var a in tiles)
            {
                if (a != null)
                {
                    if (a.inRightPlace) correctTiles++;
                }
            }

            if (correctTiles == tiles.Length - 1)
            {
                _isFinished = true;
                endPanel.SetActive(true);
                var timer = GetComponent<TimerScript>();
                timer.StopTimer();

                float totalSeconds = timer.minutes * 60f + timer.seconds;
                int finalScore = CalculateScoreFromTime(totalSeconds);

                endPanelTimeText.text = (timer.minutes < 10 ? "0" : "") + timer.minutes + ":" + (timer.seconds < 10 ? "0" : "") + timer.seconds;
                endPanelScoreText.text = $"Score: {finalScore}";

                ScoreManager.Instance.AddScore(finalScore);
            }

        }
    }

    public void Next()
    {
        SceneManager.LoadScene("Result1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Shuffle()
    {
        if (emptySpaceIndex != 15)
        {
            var tileOn15LastPos = tiles[15].targetPosition;
            tiles[15].targetPosition = emptySpace.position;
            emptySpace.position = tileOn15LastPos;
            tiles[emptySpaceIndex] = tiles[15];
            tiles[15] = null;
            emptySpaceIndex = 15;
        }

        int invertion;
        do
        {
            for (int i = 0; i < 14; i++)
            {
                if (tiles[i] != null)
                {
                    var lastPos = tiles[i].targetPosition;
                    int randomIndex = Random.Range(0, 14);
                    tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                    tiles[randomIndex].targetPosition = lastPos;
                    var tile = tiles[i];
                    tiles[i] = tiles[randomIndex];
                    tiles[randomIndex] = tile;
                }

            }

            invertion = GetInversion();
            Debug.Log("Puzzle Shuffled");

        } while (invertion % 2 != 0);
    }
    public int findIndex(PuzzleTile ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    int GetInversion()
    {
        int inversionSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInvertion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInvertion++;
                    }
                }
            }
            inversionSum += thisTileInvertion;
        }
        return inversionSum;
    }
    public void OnButtonClick()
    {
        clickCount++;

        if (clickCount == 1)
        {
            PrototypePanel.SetActive(true);
        }
        else if (clickCount == 2)
        {
            image1.SetActive(true);
        }
        else if (clickCount == 3)
        {
            image1.SetActive(false);
            image2.SetActive(true);
        }
        else if (clickCount == 4)
        {
            PrototypePanel.SetActive(false);
            image1.SetActive(false);
            image2.SetActive(false);
            clickCount = 0;
        }
    }

    private int CalculateScoreFromTime(float timeInSeconds)
    {
        if (timeInSeconds <= 90f) return 100;
        if (timeInSeconds >= 360f) return 10;

        float t = (timeInSeconds - 90f) / (360f - 90f);
        return Mathf.RoundToInt(Mathf.Lerp(100f, 10f, t));
    }

    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    // Tutorial
    public void ToggleTutorialPanel()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(!tutorialPanel.activeSelf);
            currentTutorialPage = 0;
            ShowTutorialPage(currentTutorialPage);
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

    public void PreviousTutorialPage()
    {
        if (currentTutorialPage > 0)
        {
            currentTutorialPage--;
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
