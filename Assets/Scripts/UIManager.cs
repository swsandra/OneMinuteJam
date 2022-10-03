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
    float multiplierH;
    float multiplierS;
    float multiplierV;



    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        Color.RGBToHSV(multiplierText.outlineColor, out multiplierH, out multiplierS, out multiplierV);
    }

    public void UpdateUITime(int newTime) {
        clockHand.eulerAngles = new Vector3(0,0, newTime * 6);
    }

    public void UpdateUIScore(int newScore) {
        scoreText.text = newScore.ToString();
    }

    public void UpdateUIMultiplier(int newMultiplier) {
        multiplierText.text = "x" + newMultiplier.ToString();
        multiplierText.fontSize = multiplierInitialSize + (newMultiplier-1) * multiplierIncrement;
        multiplierText.color = Color.HSVToRGB(multiplierH, (newMultiplier-1) * (multiplierS/4), multiplierV);
    }

}
