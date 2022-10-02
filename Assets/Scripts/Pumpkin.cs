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

    [SerializeField] float speed;
    [SerializeField] Sprite uncarvedSprite;
    [SerializeField] Sprite unlitSprite;
    [SerializeField] Sprite LitSprite;

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
    }

    void Update()
    {
        transform.position += transform.right*direction * Time.deltaTime * speed;
        bool outOfScreen = direction > 0 ? transform.position.x > rightLimit : transform.position.x < leftLimit;
        if (outOfScreen){
            Destroy(gameObject, .1f);
        }
    }

    public void ChangePumpkinType(PumpkinType newType){
        if (pumpkinType == PumpkinType.Uncarved && newType == PumpkinType.Unlit){
            spriteRenderer.sprite = unlitSprite;
            pumpkinType = PumpkinType.Unlit;
        } else if (pumpkinType == PumpkinType.Unlit && newType == PumpkinType.Lit){
            spriteRenderer.sprite = LitSprite;
            pumpkinType = PumpkinType.Lit;
        }
    }
}
