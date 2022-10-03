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

    [Header("Game Over UI")]
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text carvedText;
    [SerializeField] TMP_Text litText;
    [SerializeField] TMP_Text explodedText;
    [SerializeField] TMP_Text missedText;
    [SerializeField] TMP_Text maxComboText;

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

    public void UpdateGameOverScreenScore(int finalScore, int carvedPumpkins, int litPumpkins, int explodedPumpkins, int missedPumpkins, int maxCombo) {
        finalScoreText.text = finalScore.ToString();
        carvedText.text = carvedPumpkins.ToString();
        litText.text = litPumpkins.ToString();
        explodedText.text = explodedPumpkins.ToString();
        missedText.text = missedPumpkins.ToString();
        maxComboText.text = maxCombo.ToString();
    }

}
