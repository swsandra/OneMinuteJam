using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsScript : MonoBehaviour
{
    [SerializeField] Transform spriteLight1;
    [SerializeField] Transform spriteLight2;
    [SerializeField] Transform spotLight1;
    [SerializeField] Transform spotLight2;
    int direction = 1;

    // Update is called once per frame
    void Update()
    {
        spriteLight1.eulerAngles += new Vector3(0,0,20) * Time.deltaTime;
        spriteLight2.eulerAngles += new Vector3(0,0,20) * Time.deltaTime;
        // spriteLight1.transform.Rotate(new Vector3(0,0,2));
        // spriteLight2.transform.Rotate(new Vector3(0,0,2));
        if(spotLight1.localRotation.eulerAngles.z > 210 || spotLight1.localRotation.eulerAngles.z < 150)
            direction*=-1;
        spotLight1.eulerAngles += new Vector3(0,0,20*direction) * Time.deltaTime;
        spotLight2.eulerAngles += new Vector3(0,0,20*-direction) * Time.deltaTime;
        // spotLight1.transform.Rotate(new Vector3(0,0,2*-direction));
        // spotLight2.transform.Rotate(new Vector3(0,0,2*-direction));

        
    }
}
