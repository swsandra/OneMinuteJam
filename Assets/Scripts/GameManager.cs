using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Timer")]
    [SerializeField] int time = 60;
    
    int score;
    public int carvedPumpkins = 0;
    public int litPumpkins = 0;
    public int explodedPumpkins = 0;

    public int Score {
        get => score;
        set {
            score = value;
            UIManager.instance.UpdateUIScore(score);
        }
    }
    public int Time {
        get => time;
        set {
            time = value;
            UIManager.instance.UpdateUITime(time);
        }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start() {
        StartCoroutine(CountDownRoutine());
    }

    private void Update() {
        Score = carvedPumpkins + litPumpkins + explodedPumpkins;
    }

    IEnumerator CountDownRoutine() {
        while(time >= 0) {
            yield return new WaitForSeconds(1);
            Time--;
        }

        Debug.Log("se acabo");
    }
}
