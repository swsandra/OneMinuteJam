using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D bottomBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) {
            bottomBox.enabled = true;
            StartCoroutine(interactionCooldown());
        }
    }

    IEnumerator interactionCooldown() {
        yield return new WaitForSeconds(0.1f);
        bottomBox.enabled = false;
    }
}
