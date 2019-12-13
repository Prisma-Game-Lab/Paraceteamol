using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string soundPortal;
    public GameObject exitPoint;
    public Portal exitPortal;

    // Start is called before the first frame update
    void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Ball")){
            Debug.Log("Player entrou em portal");
            FMODUnity.RuntimeManager.PlayOneShot(soundPortal);
            col.transform.position = exitPortal.exitPoint.transform.position;
        }
    }

}
