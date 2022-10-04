using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    public enum PumpkinType {
        Green,
        Uncarved,
        Unlit,
        Lit,
    }

    public PumpkinType pumpkinType;
    public int direction; // Either 1 (right), or -1 (left)
    public bool interactable;

    public float speed;
    [SerializeField] Sprite uncarvedSprite;
    [SerializeField] Sprite unlitSprite;
    [SerializeField] Sprite LitSprite;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip cutSound;
    [SerializeField] AudioClip lightSound;
    [SerializeField] AudioClip wrongSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] GameObject candle;

    float leftLimit;
    float rightLimit;
    float rightMiss = 0.5f;
    float leftMiss = -0.5f;
    bool missed = false;

    SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start() {
        leftLimit = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        rightLimit = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0f, 0f)).x;
        interactable = true;
    }

    void Update()
    {
        transform.position += transform.right*direction * Time.deltaTime * speed;
        bool outOfScreen = direction > 0 ? transform.position.x > rightLimit : transform.position.x < leftLimit;
        if (outOfScreen){
            Destroy(gameObject, .1f);
        }
        bool missLimit = direction > 0 ? transform.position.x > rightMiss : transform.position.x < leftMiss;
        if (!missed && missLimit && interactable) {
            GameManager.instance.missPumpkin();
            missed = true;
        }
    }

    public void ChangePumpkinType(PumpkinType newType, bool playSound = true){
        if (pumpkinType == newType && !playSound) return;   // Instantiation
        if (pumpkinType == PumpkinType.Uncarved && newType == PumpkinType.Unlit){ // Carve pumpkin
            spriteRenderer.sprite = unlitSprite;
            pumpkinType = PumpkinType.Unlit;
        } else if (pumpkinType == PumpkinType.Unlit && newType == PumpkinType.Lit){ // Lit pumpkin
            spriteRenderer.sprite = LitSprite;
            pumpkinType = PumpkinType.Lit;
            candle.SetActive(true);
        }
    }

    [ContextMenu("Carve")]
    public void Carve(){
        if (pumpkinType != PumpkinType.Uncarved) {
            WrongClassification();
        }
        else {
            AudioSource.PlayClipAtPoint(cutSound, transform.position);
            ChangePumpkinType(PumpkinType.Unlit);
            GameManager.instance.CarvedPumpkins++;
        }
    }

    [ContextMenu("Lit")]
    public void Lit(){
        if (pumpkinType != PumpkinType.Unlit) {
            WrongClassification();
        }
        else {
            AudioSource.PlayClipAtPoint(lightSound, transform.position);
            ChangePumpkinType(PumpkinType.Lit);
            GameManager.instance.LitPumpkins++;
        }
    }

    [ContextMenu("Explode")]
    public void Explode(){
        if (pumpkinType != PumpkinType.Green){
            WrongClassification();
            return;
        }
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(gameObject, .1f);
        GameObject go = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        go.GetComponent<Explosion>().direction = direction;
        go.GetComponent<Explosion>().speed = speed;
        go.GetComponent<Explosion>().leftLimit = leftLimit;
        go.GetComponent<Explosion>().rightLimit = rightLimit;
        GameManager.instance.ExplodedPumpkins++;
    }

    void WrongClassification(){
        GameManager.instance.missPumpkin();
        AudioSource.PlayClipAtPoint(wrongSound, transform.position);
        spriteRenderer.color = new Color(.1f, .1f, .1f);
    }

}
