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

    [Header("MultiplierUI")]
    [SerializeField] TMP_Text multiplierText;
    [SerializeField] int multiplierInitialSize = 12;
    [SerializeField] int multiplierIncrement = 12;

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

    public void UpdateUIMultiplier(int newMultiplier) {
        multiplierText.text = "x" + newMultiplier.ToString();
        multiplierText.fontSize = multiplierInitialSize + newMultiplier * multiplierIncrement;
    }

}
