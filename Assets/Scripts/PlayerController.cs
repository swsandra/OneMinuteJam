using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform actionPoint;
    [Header("HardMode")]
    [SerializeField] bool hardMode;
    [SerializeField] float actionPointDown = -0.2f;
    [SerializeField] float actionPointUp = 0.3f;
    CinemachineImpulseSource impulse;
    public bool maxMultiplier = false; 

    // Start is called before the first frame update
    void Start()
    {
        impulse = transform.GetComponent<CinemachineImpulseSource>();
    }

    void shakeCamera() {
        impulse.GenerateImpulse();
    }

    public void EndExplosion() {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (hardMode && Input.GetAxisRaw("Vertical") > 0f) {
            // Change sprite to back
            GetComponent<Animator>().SetBool("facingDown", false);
            // Debug.Log(Input.GetAxisRaw("Vertical"));

            // move actionPoint up
            actionPoint.localPosition = new Vector2(0, actionPointUp);
        }
        else if (hardMode && Input.GetAxisRaw("Vertical") < 0f) {
            // Change sprite to front
            GetComponent<Animator>().SetBool("facingDown", true);
            // Debug.Log(Input.GetAxisRaw("Vertical"));
            
            // move actionPoint down
            actionPoint.localPosition = new Vector2(0, actionPointDown);

        }
        if (Input.GetKeyDown(KeyCode.J)) {
            Collider2D col = Physics2D.OverlapPoint(actionPoint.position);  // Check Collision
            if (col && col.GetComponent<Pumpkin>().interactable) {
                if(maxMultiplier)
                    shakeCamera();
                col.GetComponent<Pumpkin>().Carve();
                col.GetComponent<Pumpkin>().interactable = false;
                }
        }
        else if (Input.GetKeyDown(KeyCode.K)) {
            Collider2D col = Physics2D.OverlapPoint(actionPoint.position);  // Check Collision
            if (col && col.GetComponent<Pumpkin>().interactable) {
                if(maxMultiplier)
                    shakeCamera();
                col.GetComponent<Pumpkin>().Lit();
                col.GetComponent<Pumpkin>().interactable = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            Collider2D col = Physics2D.OverlapPoint(actionPoint.position);  // Check Collision
            if (col && col.GetComponent<Pumpkin>().interactable) {
                if(maxMultiplier)
                    shakeCamera();
                col.GetComponent<Pumpkin>().Explode();
                col.GetComponent<Pumpkin>().interactable = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Title");
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
