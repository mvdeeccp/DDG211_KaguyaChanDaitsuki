using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public int seconds, minutes;
    [SerializeField] private TextMeshProUGUI timeText;


    void Start()
    {
        AddToSecond();
    }

    private void AddToSecond()
    {
        seconds++;
        if (seconds > 59)
        {
            minutes++;
            seconds = 0;
        }
        timeText.text = (minutes < 10?"0":"") + minutes + ":" + (seconds < 10? "0":"") + seconds;
        Invoke(nameof(AddToSecond), time: 1);
    }

    public void StopTimer()
    {
        CancelInvoke(nameof(AddToSecond));
        timeText.gameObject.SetActive(false);
    }
}
