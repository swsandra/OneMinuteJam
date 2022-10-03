using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    public AnimatedTile tile;
    public bool interactable;

    [SerializeField] float speed;
    [SerializeField] Sprite uncarvedSprite;
    [SerializeField] Sprite unlitSprite;
    [SerializeField] Sprite LitSprite;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioClip cutSound;
    [SerializeField] AudioClip lightSound;
    [SerializeField] AudioClip wrongSound;
    [SerializeField] AudioClip explosionSound;

    float leftLimit;
    float rightLimit;

    SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start() {
        speed = tile.m_MinSpeed/2;
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
    }

    public void ChangePumpkinType(PumpkinType newType, bool playSound = true){
        if (pumpkinType == newType && !playSound) return;   // Instantiation
        if (pumpkinType == PumpkinType.Uncarved && newType == PumpkinType.Unlit){ // Carve pumpkin
            spriteRenderer.sprite = unlitSprite;
            pumpkinType = PumpkinType.Unlit;
        } else if (pumpkinType == PumpkinType.Unlit && newType == PumpkinType.Lit){ // Lit pumpkin
            spriteRenderer.sprite = LitSprite;
            pumpkinType = PumpkinType.Lit;
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
            GameManager.instance.carvedPumpkins++;
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
            GameManager.instance.litPumpkins++;
        }
    }

    void WrongClassification(){
        AudioSource.PlayClipAtPoint(wrongSound, transform.position);
        spriteRenderer.color = new Color(.1f, .1f, .1f);
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
        GameManager.instance.explodedPumpkins++;
    }

}
