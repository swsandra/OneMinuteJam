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
    [SerializeField] GameObject multiplier;
    [SerializeField] GameObject multiplierChildren;
    [SerializeField] int multiplierInitialSize;
    [SerializeField] int multiplierIncrement;
    float multiplierH;
    float multiplierS;
    float multiplierV;
    int direction = 1;

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
        if(newMultiplier == 1) {
            multiplier.SetActive(false);
            multiplierChildren.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        } else {
            multiplier.SetActive(true);
        }
        multiplierText.text = "x" + newMultiplier.ToString();
        multiplierText.fontSize = multiplierInitialSize + (newMultiplier-1) * multiplierIncrement;
        multiplierText.color = Color.HSVToRGB(multiplierH, (newMultiplier-1) * (multiplierS/4), multiplierV);
    }

    public void UpdateGameOverScreenScore(int finalScore, int carvedPumpkins, int litPumpkins, int explodedPumpkins, int missedPumpkins, int maxCombo) {
        finalScoreText.text = finalScore.ToString();
        carvedText.text = carvedPumpkins.ToString();
        litText.text = litPumpkins.ToString();
        explodedText.text = explodedPumpkins.ToString();
        missedText.text = missedPumpkins.ToString();
        maxComboText.text = maxCombo.ToString();
    }

    void Update() {
        if(multiplierText.text == "x5"){
            if(multiplierChildren.GetComponent<RectTransform>().eulerAngles.z > 30 && multiplierChildren.GetComponent<RectTransform>().eulerAngles.z < 330)
                direction*=-1;
            multiplierChildren.GetComponent<RectTransform>().eulerAngles += new Vector3(0,0,2*direction);
        }
    }
}
