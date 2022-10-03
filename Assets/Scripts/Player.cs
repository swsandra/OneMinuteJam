using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform actionPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0f) {
            // Change sprite to back
            GetComponent<Animator>().SetBool("facingDown", false);
            Debug.Log(Input.GetAxisRaw("Vertical"));

            // move actionPoint up
        }
        else if (Input.GetAxisRaw("Vertical") < 0f) {
            // Change sprite to front
            GetComponent<Animator>().SetBool("facingDown", true);
            Debug.Log(Input.GetAxisRaw("Vertical"));
            
            // move actionPoint down
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            Collider2D col = Physics2D.OverlapPoint(actionPoint.position);  // Check Collision
            if (col && col.GetComponent<Pumpkin>().interactable) {
                col.GetComponent<Pumpkin>().Carve();
                col.GetComponent<Pumpkin>().interactable = false;
                }
        }
        else if (Input.GetKeyDown(KeyCode.K)) {
            Collider2D col = Physics2D.OverlapPoint(actionPoint.position);  // Check Collision
            if (col && col.GetComponent<Pumpkin>().interactable) {
                col.GetComponent<Pumpkin>().Lit();
                col.GetComponent<Pumpkin>().interactable = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.L)) {
            Collider2D col = Physics2D.OverlapPoint(actionPoint.position);  // Check Collision
            if (col && col.GetComponent<Pumpkin>().interactable) {
                col.GetComponent<Pumpkin>().Explode();
                col.GetComponent<Pumpkin>().interactable = false;
            }
        }
    }
}
