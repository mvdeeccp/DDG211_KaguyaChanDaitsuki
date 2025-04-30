using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TextSwitcher : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;            // Text ที่แสดงข้อความ
    public string[] messages;            // ข้อความแต่ละหน้า
    public string nextSceneName;         // ชื่อ Scene ถัดไป

    private int currentIndex = 0;

    void Start()
    {
        if (messages.Length > 0)
        {
            tutorialText.text = messages[0];
        }
    }

    public void ShowNextText()
    {
        currentIndex++;

        if (currentIndex < messages.Length)
        {
            tutorialText.text = messages[currentIndex];
        }
        else
        {
            // เมื่อครบข้อความทั้งหมด ให้ไป Scene ถัดไป
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
