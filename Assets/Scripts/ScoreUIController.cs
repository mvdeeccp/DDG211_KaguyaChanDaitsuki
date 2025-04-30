using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIController : MonoBehaviour
{
    public Image scoreFillImage;
    //public Text scoreText;     

    private void Update()
    {
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (ScoreManager.Instance != null)
        {
            float fillAmount = (float)ScoreManager.Instance.currentScore / ScoreManager.Instance.maxScore;
            scoreFillImage.fillAmount = fillAmount;
        }
    }
}
