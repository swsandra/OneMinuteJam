using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;    
    [Header("Timer")]
    [SerializeField] int time = 60;
    
    int score;
    int carvedPumpkins = 0;
    int litPumpkins = 0;
    int explodedPumpkins = 0;
    [SerializeField] int missedPumpkins = 0;
    [SerializeField] int combo = 0;
    [SerializeField] int comboMultiplier = 1;
    [SerializeField] int maxCombo = 0;
    [SerializeField] AudioClip drumstick;
    [SerializeField] AudioClip gameSong;

    float beatTime = 0.5357f;

    public int Score {
        get => score;
        set {
            combo++;
            maxCombo = Mathf.Max(combo, maxCombo);
            score = value;
            if (combo % 5 == 0) {
                comboMultiplier = Mathf.Clamp(comboMultiplier + 1, 1, 5);
            }
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
    public int CarvedPumpkins {
        get => carvedPumpkins;
        set {
            carvedPumpkins = value;
            Score += comboMultiplier;
        }
    }
    public int LitPumpkins {
        get => litPumpkins;
        set {
            litPumpkins = value;
            Score += comboMultiplier;
        }
    }
    public int ExplodedPumpkins {
        get => explodedPumpkins;
        set {
            explodedPumpkins = value;
            Score += comboMultiplier;
        }
    }
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start() {
        StartCoroutine(prepareToStart());
    }

    public void missPumpkin() {
        combo = 0;
        comboMultiplier = 1;
        missedPumpkins++;
    }

    IEnumerator prepareToStart() {
        AudioSource.PlayClipAtPoint(drumstick, transform.position);
        yield return new WaitForSeconds(beatTime);
        AudioSource.PlayClipAtPoint(drumstick, transform.position);
        yield return new WaitForSeconds(beatTime);
        AudioSource.PlayClipAtPoint(drumstick, transform.position);
        yield return new WaitForSeconds(beatTime);
        AudioSource.PlayClipAtPoint(drumstick, transform.position);
        yield return new WaitForSeconds(beatTime);
        AudioSource.PlayClipAtPoint(gameSong, transform.position);
        StartCoroutine(CountDownRoutine());
    }

    IEnumerator CountDownRoutine() {
        while(time >= 0) {
            yield return new WaitForSeconds(1);
            Time--;
        }

        Debug.Log("se acabo");
    }
}
