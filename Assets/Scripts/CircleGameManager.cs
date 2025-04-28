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
    //public TextMeshProUGUI finishText;

    private int currentIndex = -1;
    private float responseTime = 1.3f;
    private float minResponseTime = 0.5f;
    private float decreaseStep = 0.1f;
    private bool isWaitingForClick = false;
    
    private int roundCount = 0;
    private int maxRounds = 20;

    public GameObject finishPanel;

    public GameObject Image1;
    public GameObject Image2;
    public GameObject Image3;
    public GameObject Image4;
    public GameObject Image5;

    private int correctClickCount = 0;

    public TextMeshProUGUI resultText;



    void Start()
    {
        finishPanel.SetActive(false);
        foreach (var circle in circles)
        {
            Button btn = circle.button;
            btn.onClick.AddListener(() => OnCircleClick(btn));
            circle.border.SetActive(false);
        }

        //finishText.gameObject.SetActive(false);
        resultText.text = "";
        StartCoroutine(RandomHighlightRoutine());
    }

    IEnumerator RandomHighlightRoutine()
    {
        while (roundCount < maxRounds)
        {
            float randomDelay = Random.Range(1.5f, 2.1f);
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
                resultText.text = "Miss!";
                resultText.color = Color.red;
                /*Image borderImage = circles[currentIndex].border.GetComponent<Image>();
                borderImage.color = Color.red;*/

                yield return new WaitForSeconds(0.5f);
                resultText.text = "";
                ResetHighlight();
                responseTime = Mathf.Max(minResponseTime, responseTime - decreaseStep);
                isWaitingForClick = false;
            }

            roundCount++;
        }

        Debug.Log("Finish!");
        //finishText.gameObject.SetActive(true);
        finishPanel.SetActive(true);
    }


    void OnCircleClick(Button clickedButton)
    {

        if (!isWaitingForClick) return;

        if (circles[currentIndex].button == clickedButton)
        {
            Debug.Log("Correct!");
            resultText.text = "Correct!";
            resultText.color = Color.green;
            StartCoroutine(ClearResultTextAfterDelay(0.5f));
            /*Image borderImage = circles[currentIndex].border.GetComponent<Image>();
            borderImage.color = Color.green;

            StartCoroutine(ResetHighlightAfterDelay(0.1f));*/
            ResetHighlight();
            isWaitingForClick = false;
            correctClickCount++;

            CheckPanelSwitch();
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

    void CheckPanelSwitch()
    {
        if (correctClickCount == 5)
        {
            Image1.SetActive(false);
            Image2.SetActive(true);
        }
        else if (correctClickCount == 10)
        {
            Image2.SetActive(false);
            Image3.SetActive(true);
        }
        else if (correctClickCount == 14)
        {
            Image3.SetActive(false);
            Image4.SetActive(true);
        }
        else if (correctClickCount == 19)
        {
            Image4.SetActive(false);
            Image5.SetActive(true);
        }
    }
    IEnumerator ClearResultTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        resultText.text = "";
    }
    /* IEnumerator ResetHighlightAfterDelay(float delay)
     {
         yield return new WaitForSeconds(delay);
         ResetHighlight();
     }*/

}
