using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CircleGameManager : MonoBehaviour
{
    [System.Serializable]
    public class Circle
    {
        public Button button;
        public GameObject border;
    }

    public List<Circle> circles;
    public TextMeshProUGUI finishText;

    private int currentIndex = -1;
    private float responseTime = 1f;
    private float minResponseTime = 0.5f;
    private float decreaseStep = 0.1f;
    private bool isWaitingForClick = false;

    private int roundCount = 0;
    private int maxRounds = 20;

    public GameObject finishPanel;

    void Start()
    {
        finishPanel.SetActive(false);
        foreach (var circle in circles)
        {
            Button btn = circle.button;
            btn.onClick.AddListener(() => OnCircleClick(btn));
            circle.border.SetActive(false);
        }

        finishText.gameObject.SetActive(false);
        StartCoroutine(RandomHighlightRoutine());
    }

    IEnumerator RandomHighlightRoutine()
    {
        while (roundCount < maxRounds)
        {
            float randomDelay = Random.Range(1f, 2.1f);
            yield return new WaitForSeconds(randomDelay);

            currentIndex = Random.Range(0, circles.Count);
            HighlightCircle(currentIndex);

            isWaitingForClick = true;
            float timer = responseTime;

            while (timer > 0f)
            {
                timer -= Time.deltaTime;
                if (!isWaitingForClick) break;
                yield return null;
            }

            if (isWaitingForClick)
            {
                Debug.Log("Miss!");
                ResetHighlight();
                responseTime = Mathf.Max(minResponseTime, responseTime - decreaseStep);
                isWaitingForClick = false;
            }

            roundCount++;
        }

        Debug.Log("Finish!");
        finishText.gameObject.SetActive(true);
        finishPanel.SetActive(true);
    }


    void OnCircleClick(Button clickedButton)
    {
        if (!isWaitingForClick) return;

        if (circles[currentIndex].button == clickedButton)
        {
            Debug.Log("Correct!");
            ResetHighlight();
            isWaitingForClick = false;
        }
        else
        {
            Debug.Log("Wrong!");
        }
    }

    void HighlightCircle(int index)
    {
        ResetHighlight();
        circles[index].border.SetActive(true);
    }

    void ResetHighlight()
    {
        foreach (var circle in circles)
        {
            circle.border.SetActive(false);
        }

    }
    public void ReloadScene()
    {
        SceneManager.LoadScene("Game4");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
