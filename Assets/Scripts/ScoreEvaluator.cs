using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreEvaluator : MonoBehaviour
{
    public int thresholdScore = 235;

    public void EvaluateAndLoadResultScene()
    {
        if (ScoreManager.Instance == null)
        {
            Debug.LogWarning("ScoreManager instance not found.");
            return;
        }

        int totalScore = ScoreManager.Instance.currentScore;

        if (totalScore >= thresholdScore)
        {
            SceneManager.LoadScene("Result1"); // เปลี่ยนชื่อ scene ให้ตรงกับที่คุณตั้งไว้ใน Build Settings
        }
        else
        {
            SceneManager.LoadScene("Result2");
        }
    }
}
