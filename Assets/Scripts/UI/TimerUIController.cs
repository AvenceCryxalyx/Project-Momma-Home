using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Image timerHand;

    void Update()
    {
        timeText.text = TimeManager.instance.TimeFormatted.ToString(@"mm\:ss\:ff");
    }
}
