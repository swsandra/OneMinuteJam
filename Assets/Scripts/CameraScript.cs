using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform player;

    [ContextMenu("ZoomIn")]
    public void ZoomToPlayer() {
        StartCoroutine(1f.Tweeng((p)=>transform.position=p, transform.position, new Vector3(player.position.x,player.position.y, -10)));
        StartCoroutine(1f.Tweeng((s)=>GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize=s, 3f, 1f));
    }

    [ContextMenu("ZoomOut")]
    public void ZoomOut() {
        StartCoroutine(.5f.Tweeng((p)=>transform.position=p, transform.position, new Vector3(0,0,-10)));
        StartCoroutine(1f.Tweeng((s)=>GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize=s, 1f, 3f));

    }
}
