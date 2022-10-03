using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool canInteract = true;
    [SerializeField] Transform actionPoint;
    [SerializeField] float actionCooldown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract) {
            bool action = false;
            if(Input.GetKeyDown(KeyCode.J)) {
                Collider2D col = Physics2D.OverlapPoint(actionPoint.position);  // Check Collision
                if (col && col.GetComponent<Pumpkin>().interactable) {
                    col.GetComponent<Pumpkin>().Carve();
                    col.GetComponent<Pumpkin>().interactable = false;
                    action = true;
                }
            }
            else if(Input.GetKeyDown(KeyCode.K)) {
                Collider2D col = Physics2D.OverlapPoint(actionPoint.position);  // Check Collision
                if (col && col.GetComponent<Pumpkin>().interactable) {
                    col.GetComponent<Pumpkin>().Lit();
                    col.GetComponent<Pumpkin>().interactable = false;
                    action = true;
                }
            }
            else if(Input.GetKeyDown(KeyCode.L)) {
                Collider2D col = Physics2D.OverlapPoint(actionPoint.position);  // Check Collision
                if (col && col.GetComponent<Pumpkin>().interactable) {
                    col.GetComponent<Pumpkin>().Explode();
                    col.GetComponent<Pumpkin>().interactable = false;
                    action = true;
                }
            }
            if (action) {
                canInteract = false;
                StartCoroutine(interactionCooldown());
            }
        }
    }

    IEnumerator interactionCooldown() {
        yield return new WaitForSeconds(actionCooldown);
        canInteract = true;
    }
}
