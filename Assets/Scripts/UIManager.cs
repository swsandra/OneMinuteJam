using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("ClockUI")]
    [SerializeField] Transform clockHand;

    [Header("ScoreUI")]
    [SerializeField] TMP_Text scoreText;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void UpdateUITime(int newTime) {
        clockHand.eulerAngles = new Vector3(0,0, newTime * 6);
    }

    public void UpdateUIScore(int newScore) {
        scoreText.text = newScore.ToString();
    }

}
