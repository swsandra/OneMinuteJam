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
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameUICanvas;
    [SerializeField] GameObject pumpkinSpawner;
    [SerializeField] Transform player;
    [SerializeField] GameObject lights;

    float beatTime = 0.5357f;

    public int Score {
        get => score;
        set {
            combo++;
            maxCombo = Mathf.Max(combo, maxCombo);
            score = value;
            if (combo % 5 == 0) {
                ComboMultiplier = Mathf.Clamp(ComboMultiplier + 1, 1, 5);
            }
            UIManager.instance.UpdateUIScore(score);
        }
    }
    public int ComboMultiplier {
        get => comboMultiplier;
        set {
            comboMultiplier = value;
            if (comboMultiplier == 5 ) {
                player.GetComponent<PlayerController>().maxMultiplier = true;
                lights.SetActive(true);
            }
            UIManager.instance.UpdateUIMultiplier(comboMultiplier);
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
            Score += ComboMultiplier;
        }
    }
    public int LitPumpkins {
        get => litPumpkins;
        set {
            litPumpkins = value;
            Score += ComboMultiplier;
        }
    }
    public int ExplodedPumpkins {
        get => explodedPumpkins;
        set {
            explodedPumpkins = value;
            Score += ComboMultiplier;
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
        ComboMultiplier = 1;
        player.GetComponent<PlayerController>().maxMultiplier = false;
        lights.SetActive(false);
        missedPumpkins++;
    }

    IEnumerator prepareToStart() {
        yield return new WaitForSeconds(1f);
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
        StartCoroutine(GameOverCoroutine());
    }
        
    [ContextMenu("Game Over")]
    IEnumerator GameOverCoroutine() {
        player.GetComponent<PlayerController>().enabled = false;
        UIManager.instance.UpdateGameOverScreenScore(score, carvedPumpkins, litPumpkins, explodedPumpkins, missedPumpkins, maxCombo);
        yield return new WaitForSeconds(1);
        FindObjectOfType<CameraScript>().ZoomToPlayer();
        yield return new WaitForSeconds(2);
        player.GetComponent<Animator>().Play("Cry");
        yield return new WaitForSeconds(1);
        player.GetComponent<Animator>().Play("Explosion");
        player.localScale = new Vector3(2,2,2);
        yield return new WaitForSeconds(0.5f);
        player.gameObject.SetActive(false);
        FindObjectOfType<CameraScript>().ZoomOut();
        yield return new WaitForSeconds(2f);
        pumpkinSpawner.SetActive(false);
        gameUICanvas.SetActive(false);
        gameOverScreen.SetActive(true);
    }
}
